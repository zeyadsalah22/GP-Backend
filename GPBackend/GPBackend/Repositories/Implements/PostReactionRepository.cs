using GPBackend.Models;
using GPBackend.Models.Enums;
using GPBackend.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace GPBackend.Repositories.Implements
{
    public class PostReactionRepository : IPostReactionRepository
    {
        private readonly GPDBContext _context;

        public PostReactionRepository(GPDBContext context)
        {
            _context = context;
        }

        public async Task<PostReaction?> GetByIdAsync(int postReactionId)
        {
            return await _context.PostReactions
                .Include(pr => pr.User)
                .Include(pr => pr.Post)
                .FirstOrDefaultAsync(pr => pr.PostReactionId == postReactionId);
        }

        public async Task<PostReaction?> GetByPostAndUserAsync(int postId, int userId)
        {
            return await _context.PostReactions
                .FirstOrDefaultAsync(pr => pr.PostId == postId && pr.UserId == userId);
        }

        public async Task<List<PostReaction>> GetByPostIdAsync(int postId)
        {
            return await _context.PostReactions
                .Where(pr => pr.PostId == postId)
                .Include(pr => pr.User)
                .ToListAsync();
        }

        public async Task<List<PostReaction>> GetByPostIdWithUsersAsync(int postId, int pageNumber, int pageSize)
        {
            return await _context.PostReactions
                .Where(pr => pr.PostId == postId)
                .Include(pr => pr.User)
                .OrderByDescending(pr => pr.CreatedAt)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
        }

        public async Task<Dictionary<ReactionType, int>> GetReactionCountsByPostIdAsync(int postId)
        {
            var reactions = await _context.PostReactions
                .Where(pr => pr.PostId == postId)
                .GroupBy(pr => pr.ReactionType)
                .Select(g => new { ReactionType = g.Key, Count = g.Count() })
                .ToListAsync();

            var result = new Dictionary<ReactionType, int>
            {
                { ReactionType.UPVOTE, 0 },
                { ReactionType.DOWNVOTE, 0 },
                { ReactionType.HELPFUL, 0 },
                { ReactionType.INSIGHTFUL, 0 },
                { ReactionType.THANKS, 0 }
            };

            foreach (var reaction in reactions)
            {
                result[reaction.ReactionType] = reaction.Count;
            }

            return result;
        }

        public async Task<int> GetTotalReactionCountByPostIdAsync(int postId)
        {
            return await _context.PostReactions
                .Where(pr => pr.PostId == postId)
                .CountAsync();
        }

        public async Task<PostReaction> AddAsync(PostReaction postReaction)
        {
            _context.PostReactions.Add(postReaction);
            await _context.SaveChangesAsync();
            return postReaction;
        }

        public async Task<PostReaction> UpdateAsync(PostReaction postReaction)
        {
            postReaction.UpdatedAt = DateTime.UtcNow;
            _context.PostReactions.Update(postReaction);
            await _context.SaveChangesAsync();
            return postReaction;
        }

        public async Task DeleteAsync(PostReaction postReaction)
        {
            _context.PostReactions.Remove(postReaction);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> ExistsAsync(int postId, int userId)
        {
            return await _context.PostReactions
                .AnyAsync(pr => pr.PostId == postId && pr.UserId == userId);
        }
    }
}

