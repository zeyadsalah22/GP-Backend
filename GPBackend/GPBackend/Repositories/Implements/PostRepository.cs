using GPBackend.Models;
using GPBackend.Repositories.Interfaces;
using GPBackend.DTOs.Post;
using GPBackend.DTOs.Common;
using Microsoft.EntityFrameworkCore;

namespace GPBackend.Repositories.Implements
{
    public class PostRepository : IPostRepository
    {
        private readonly GPDBContext _context;

        public PostRepository(GPDBContext context)
        {
            _context = context;
        }

        public async Task<PagedResult<Post>> GetFilteredAsync(PostQueryDto queryDto)
        {
            IQueryable<Post> query = _context.Posts
                .Include(p => p.User)
                .Include(p => p.PostTags)
                    .ThenInclude(pt => pt.Tag)
                .Where(p => !p.IsDeleted);

            // Apply search
            if (!string.IsNullOrWhiteSpace(queryDto.SearchTerm))
            {
                string searchTerm = queryDto.SearchTerm.ToLower();
                query = query.Where(p =>
                    (p.Title != null && p.Title.ToLower().Contains(searchTerm)) ||
                    p.Content.ToLower().Contains(searchTerm)
                );
            }

            // Apply filters
            if (queryDto.PostType.HasValue)
            {
                query = query.Where(p => p.PostType == queryDto.PostType.Value);
            }

            if (queryDto.Status.HasValue)
            {
                query = query.Where(p => p.Status == queryDto.Status.Value);
            }

            if (queryDto.UserId.HasValue)
            {
                query = query.Where(p => p.UserId == queryDto.UserId.Value);
            }

            if (queryDto.IsAnonymous.HasValue)
            {
                query = query.Where(p => p.IsAnonymous == queryDto.IsAnonymous.Value);
            }

            if (queryDto.CreatedAfter.HasValue)
            {
                query = query.Where(p => p.CreatedAt >= queryDto.CreatedAfter.Value);
            }

            if (queryDto.CreatedBefore.HasValue)
            {
                query = query.Where(p => p.CreatedAt <= queryDto.CreatedBefore.Value);
            }

            // Filter by tags
            if (queryDto.Tags != null && queryDto.Tags.Any())
            {
                query = query.Where(p => p.PostTags.Any(pt => queryDto.Tags.Contains(pt.Tag.Name)));
            }

            // Get total count before pagination
            int totalCount = await query.CountAsync();

            // Apply sorting
            query = ApplySorting(query, queryDto.SortBy ?? "CreatedAt", queryDto.SortDescending);

            // Apply pagination
            var items = await query
                .Skip((queryDto.PageNumber - 1) * queryDto.PageSize)
                .Take(queryDto.PageSize)
                .ToListAsync();

            return new PagedResult<Post>
            {
                Items = items,
                PageNumber = queryDto.PageNumber,
                PageSize = queryDto.PageSize,
                TotalCount = totalCount
            };
        }

        private IQueryable<Post> ApplySorting(IQueryable<Post> query, string sortBy, bool descending)
        {
            sortBy = char.ToUpper(sortBy[0]) + sortBy.Substring(1).ToLower();

            return sortBy switch
            {
                "Createdat" or "CreatedAt" => descending ? query.OrderByDescending(p => p.CreatedAt) : query.OrderBy(p => p.CreatedAt),
                "Updatedat" or "UpdatedAt" => descending ? query.OrderByDescending(p => p.UpdatedAt) : query.OrderBy(p => p.UpdatedAt),
                "Title" => descending ? query.OrderByDescending(p => p.Title) : query.OrderBy(p => p.Title),
                "Posttype" or "PostType" => descending ? query.OrderByDescending(p => p.PostType) : query.OrderBy(p => p.PostType),
                _ => descending ? query.OrderByDescending(p => p.CreatedAt) : query.OrderBy(p => p.CreatedAt)
            };
        }

        public async Task<IEnumerable<Post>> GetAllAsync()
        {
            return await _context.Posts
                .Include(p => p.User)
                .Include(p => p.PostTags)
                    .ThenInclude(pt => pt.Tag)
                .Where(p => !p.IsDeleted)
                .ToListAsync();
        }

        public async Task<Post?> GetByIdAsync(int id)
        {
            return await _context.Posts
                .Include(p => p.User)
                .Include(p => p.PostTags)
                    .ThenInclude(pt => pt.Tag)
                .Where(p => !p.IsDeleted && p.PostId == id)
                .FirstOrDefaultAsync();
        }

        public async Task<Post> CreateAsync(Post post)
        {
            _context.Posts.Add(post);
            await _context.SaveChangesAsync();
            return post;
        }

        public async Task<bool> UpdateAsync(Post post)
        {
            try
            {
                post.UpdatedAt = DateTime.UtcNow;
                _context.Posts.Update(post);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await PostExistsAsync(post.PostId))
                {
                    return false;
                }
                throw;
            }
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var post = await GetByIdAsync(id);
            if (post == null)
            {
                return false;
            }

            post.IsDeleted = true;
            post.UpdatedAt = DateTime.UtcNow;
            _context.Posts.Update(post);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<int> BulkSoftDeleteAsync(IEnumerable<int> ids)
        {
            if (ids == null) return 0;
            var idList = ids.Distinct().ToList();
            if (idList.Count == 0) return 0;

            var posts = await _context.Posts
                .Where(p => idList.Contains(p.PostId) && !p.IsDeleted)
                .ToListAsync();

            foreach (var post in posts)
            {
                post.IsDeleted = true;
                post.UpdatedAt = DateTime.UtcNow;
            }

            await _context.SaveChangesAsync();
            return posts.Count;
        }

        public async Task<IEnumerable<Post>> GetByUserIdAsync(int userId)
        {
            return await _context.Posts
                .Include(p => p.User)
                .Include(p => p.PostTags)
                    .ThenInclude(pt => pt.Tag)
                .Where(p => !p.IsDeleted && p.UserId == userId)
                .OrderByDescending(p => p.CreatedAt)
                .ToListAsync();
        }

        public async Task<IEnumerable<Post>> GetPublishedPostsAsync()
        {
            return await _context.Posts
                .Include(p => p.User)
                .Include(p => p.PostTags)
                    .ThenInclude(pt => pt.Tag)
                .Where(p => !p.IsDeleted && p.Status == Models.Enums.PostStatus.PUBLISHED)
                .OrderByDescending(p => p.CreatedAt)
                .ToListAsync();
        }

        public async Task<IEnumerable<Post>> GetDraftsByUserIdAsync(int userId)
        {
            return await _context.Posts
                .Include(p => p.User)
                .Include(p => p.PostTags)
                    .ThenInclude(pt => pt.Tag)
                .Where(p => !p.IsDeleted && p.UserId == userId && p.Status == Models.Enums.PostStatus.DRAFT)
                .OrderByDescending(p => p.CreatedAt)
                .ToListAsync();
        }

        private async Task<bool> PostExistsAsync(int id)
        {
            return await _context.Posts.AnyAsync(p => p.PostId == id);
        }
    }
}

