using GPBackend.Models;
using GPBackend.Repositories.Interfaces;
using GPBackend.DTOs.Comment;
using GPBackend.DTOs.Common;
using Microsoft.EntityFrameworkCore;

namespace GPBackend.Repositories.Implements
{
    public class CommentRepository : ICommentRepository
    {
        private readonly GPDBContext _context;

        public CommentRepository(GPDBContext context)
        {
            _context = context;
        }

        public async Task<PagedResult<Comment>> GetCommentsByPostIdAsync(CommentQueryDto queryDto)
        {
            IQueryable<Comment> query = _context.Comments
                .Include(c => c.User)
                .Include(c => c.ParentComment)
                    .ThenInclude(pc => pc!.User)
                .Include(c => c.Mentions)
                    .ThenInclude(m => m.MentionedUser)
                .Where(c => c.PostId == queryDto.PostId);

            if (queryDto.Level.HasValue)
            {
                query = query.Where(c => c.Level == queryDto.Level.Value);
            }
            else
            {
                query = query.Where(c => c.Level == 0);
            }

            int totalCount = await query.CountAsync();

            query = ApplySorting(query, queryDto.SortBy ?? "CreatedAt", queryDto.SortOrder ?? "DESC");

            var items = await query
                .Skip((queryDto.PageNumber - 1) * queryDto.PageSize)
                .Take(queryDto.PageSize)
                .ToListAsync();

            foreach (var comment in items.Where(c => c.Level == 0))
            {
                await _context.Entry(comment)
                    .Collection(c => c.Replies)
                    .Query()
                    .Include(r => r.User)
                    .Include(r => r.Mentions)
                        .ThenInclude(m => m.MentionedUser)
                    .OrderBy(r => r.CreatedAt)
                    .LoadAsync();
            }

            return new PagedResult<Comment>
            {
                Items = items,
                PageNumber = queryDto.PageNumber,
                PageSize = queryDto.PageSize,
                TotalCount = totalCount
            };
        }

        private IQueryable<Comment> ApplySorting(IQueryable<Comment> query, string sortBy, string sortOrder)
        {
            bool descending = sortOrder.ToUpper() == "DESC";

            return sortBy.ToLower() switch
            {
                "createdat" => descending ? query.OrderByDescending(c => c.CreatedAt) : query.OrderBy(c => c.CreatedAt),
                "updatedat" => descending ? query.OrderByDescending(c => c.UpdatedAt) : query.OrderBy(c => c.UpdatedAt),
                _ => descending ? query.OrderByDescending(c => c.CreatedAt) : query.OrderBy(c => c.CreatedAt)
            };
        }

        public async Task<Comment?> GetByIdAsync(int id)
        {
            return await _context.Comments
                .Include(c => c.User)
                .Include(c => c.ParentComment)
                    .ThenInclude(pc => pc!.User)
                .Include(c => c.Mentions)
                    .ThenInclude(m => m.MentionedUser)
                .Include(c => c.Post)
                .FirstOrDefaultAsync(c => c.CommentId == id);
        }

        public async Task<Comment?> GetByIdWithRepliesAsync(int id)
        {
            var comment = await _context.Comments
                .Include(c => c.User)
                .Include(c => c.ParentComment)
                    .ThenInclude(pc => pc!.User)
                .Include(c => c.Mentions)
                    .ThenInclude(m => m.MentionedUser)
                .Include(c => c.Post)
                .Include(c => c.Replies)
                    .ThenInclude(r => r.User)
                .FirstOrDefaultAsync(c => c.CommentId == id);

            return comment;
        }

        public async Task<Comment> CreateAsync(Comment comment)
        {
            _context.Comments.Add(comment);
            await _context.SaveChangesAsync();

            await _context.Entry(comment)
                .Reference(c => c.User)
                .LoadAsync();

            if (comment.ParentCommentId.HasValue)
            {
                await _context.Entry(comment)
                    .Reference(c => c.ParentComment)
                    .LoadAsync();

                if (comment.ParentComment != null)
                {
                    await _context.Entry(comment.ParentComment)
                        .Reference(pc => pc.User)
                        .LoadAsync();
                }
            }

            return comment;
        }

        public async Task<bool> UpdateAsync(Comment comment)
        {
            try
            {
                comment.UpdatedAt = DateTime.UtcNow;
                _context.Comments.Update(comment);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await CommentExistsAsync(comment.CommentId))
                {
                    return false;
                }
                throw;
            }
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var comment = await GetByIdAsync(id);
            if (comment == null)
            {
                return false;
            }

            comment.IsDeleted = true;
            comment.UpdatedAt = DateTime.UtcNow;
            _context.Comments.Update(comment);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<int> GetCommentCountByPostIdAsync(int postId)
        {
            return await _context.Comments
                .Where(c => c.PostId == postId && !c.IsDeleted && c.Level == 0)
                .CountAsync();
        }

        public async Task<List<Comment>> GetTopCommentsByPostIdAsync(int postId, int count)
        {
            return await _context.Comments
                .Include(c => c.User)
                .Where(c => c.PostId == postId && c.Level == 0)
                .OrderByDescending(c => c.CreatedAt)
                .Take(count)
                .ToListAsync();
        }

        public async Task<List<Comment>> GetRepliesByCommentIdAsync(int commentId)
        {
            return await _context.Comments
                .Include(c => c.User)
                .Include(c => c.Mentions)
                    .ThenInclude(m => m.MentionedUser)
                .Where(c => c.ParentCommentId == commentId)
                .OrderBy(c => c.CreatedAt)
                .ToListAsync();
        }

        public async Task<bool> UpdateReplyCountAsync(int commentId, int increment)
        {
            var comment = await _context.Comments.FindAsync(commentId);
            if (comment == null)
            {
                return false;
            }

            comment.ReplyCount += increment;
            if (comment.ReplyCount < 0) comment.ReplyCount = 0;

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<int> SoftDeleteAllByPostIdAsync(int postId)
        {
            var comments = await _context.Comments
                .Where(c => c.PostId == postId && !c.IsDeleted)
                .ToListAsync();

            foreach (var comment in comments)
            {
                comment.IsDeleted = true;
                comment.UpdatedAt = DateTime.UtcNow;
            }

            await _context.SaveChangesAsync();
            return comments.Count;
        }

        public async Task AddCommentEditHistoryAsync(CommentEditHistory history)
        {
            _context.CommentEditHistories.Add(history);
            await _context.SaveChangesAsync();
        }

        public async Task AddCommentMentionsAsync(List<CommentMention> mentions)
        {
            if (mentions.Any())
            {
                _context.CommentMentions.AddRange(mentions);
                await _context.SaveChangesAsync();
            }
        }

        public async Task RemoveCommentMentionsAsync(int commentId)
        {
            var mentions = await _context.CommentMentions
                .Where(m => m.CommentId == commentId)
                .ToListAsync();

            if (mentions.Any())
            {
                _context.CommentMentions.RemoveRange(mentions);
                await _context.SaveChangesAsync();
            }
        }

        private async Task<bool> CommentExistsAsync(int id)
        {
            return await _context.Comments.AnyAsync(c => c.CommentId == id);
        }
    }
}

