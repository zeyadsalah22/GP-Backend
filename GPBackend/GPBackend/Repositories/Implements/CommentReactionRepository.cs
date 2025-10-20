using GPBackend.Models;
using GPBackend.Models.Enums;
using GPBackend.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace GPBackend.Repositories.Implements
{
    public class CommentReactionRepository : ICommentReactionRepository
    {
        private readonly GPDBContext _context;

        public CommentReactionRepository(GPDBContext context)
        {
            _context = context;
        }

        public async Task<CommentReaction?> GetByIdAsync(int commentReactionId)
        {
            return await _context.CommentReactions
                .Include(cr => cr.User)
                .Include(cr => cr.Comment)
                .FirstOrDefaultAsync(cr => cr.CommentReactionId == commentReactionId);
        }

        public async Task<CommentReaction?> GetByCommentAndUserAsync(int commentId, int userId)
        {
            return await _context.CommentReactions
                .FirstOrDefaultAsync(cr => cr.CommentId == commentId && cr.UserId == userId);
        }

        public async Task<List<CommentReaction>> GetByCommentIdAsync(int commentId)
        {
            return await _context.CommentReactions
                .Where(cr => cr.CommentId == commentId)
                .Include(cr => cr.User)
                .ToListAsync();
        }

        public async Task<int> GetUpvoteCountByCommentIdAsync(int commentId)
        {
            return await _context.CommentReactions
                .Where(cr => cr.CommentId == commentId && cr.ReactionType == CommentReactionType.UPVOTE)
                .CountAsync();
        }

        public async Task<int> GetDownvoteCountByCommentIdAsync(int commentId)
        {
            return await _context.CommentReactions
                .Where(cr => cr.CommentId == commentId && cr.ReactionType == CommentReactionType.DOWNVOTE)
                .CountAsync();
        }

        public async Task<CommentReaction> AddAsync(CommentReaction commentReaction)
        {
            _context.CommentReactions.Add(commentReaction);
            await _context.SaveChangesAsync();
            return commentReaction;
        }

        public async Task<CommentReaction> UpdateAsync(CommentReaction commentReaction)
        {
            commentReaction.UpdatedAt = DateTime.UtcNow;
            _context.CommentReactions.Update(commentReaction);
            await _context.SaveChangesAsync();
            return commentReaction;
        }

        public async Task DeleteAsync(CommentReaction commentReaction)
        {
            _context.CommentReactions.Remove(commentReaction);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> ExistsAsync(int commentId, int userId)
        {
            return await _context.CommentReactions
                .AnyAsync(cr => cr.CommentId == commentId && cr.UserId == userId);
        }
    }
}

