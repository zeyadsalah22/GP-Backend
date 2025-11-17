using GPBackend.Models;
using GPBackend.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace GPBackend.Repositories.Implements
{
    public class SavedPostRepository : ISavedPostRepository
    {
        private readonly GPDBContext _context;

        public SavedPostRepository(GPDBContext context)
        {
            _context = context;
        }

        public async Task<SavedPost?> GetByIdAsync(int savedPostId)
        {
            return await _context.SavedPosts
                .Include(sp => sp.User)
                .Include(sp => sp.Post)
                    .ThenInclude(p => p.User)
                .Include(sp => sp.Post)
                    .ThenInclude(p => p.PostTags)
                        .ThenInclude(pt => pt.Tag)
                .FirstOrDefaultAsync(sp => sp.SavedPostId == savedPostId);
        }

        public async Task<SavedPost?> GetByUserAndPostAsync(int userId, int postId)
        {
            return await _context.SavedPosts
                .FirstOrDefaultAsync(sp => sp.UserId == userId && sp.PostId == postId);
        }

        public async Task<List<SavedPost>> GetByUserIdAsync(int userId)
        {
            return await _context.SavedPosts
                .Where(sp => sp.UserId == userId)
                .Include(sp => sp.Post)
                    .ThenInclude(p => p.User)
                .Include(sp => sp.Post)
                    .ThenInclude(p => p.PostTags)
                        .ThenInclude(pt => pt.Tag)
                .Where(sp => !sp.Post.IsDeleted && sp.Post.Status == Models.Enums.PostStatus.PUBLISHED)
                .OrderByDescending(sp => sp.SavedAt)
                .ToListAsync();
        }

        public async Task<SavedPost> AddAsync(SavedPost savedPost)
        {
            _context.SavedPosts.Add(savedPost);
            await _context.SaveChangesAsync();
            return savedPost;
        }

        public async Task DeleteAsync(SavedPost savedPost)
        {
            _context.SavedPosts.Remove(savedPost);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> ExistsAsync(int userId, int postId)
        {
            return await _context.SavedPosts
                .AnyAsync(sp => sp.UserId == userId && sp.PostId == postId);
        }
    }
}

