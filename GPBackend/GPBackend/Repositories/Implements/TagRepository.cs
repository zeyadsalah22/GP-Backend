using GPBackend.Models;
using GPBackend.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace GPBackend.Repositories.Implements
{
    public class TagRepository : ITagRepository
    {
        private readonly GPDBContext _context;

        public TagRepository(GPDBContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Tag>> GetAllAsync()
        {
            return await _context.Tags
                .OrderBy(t => t.Name)
                .ToListAsync();
        }

        public async Task<Tag?> GetByIdAsync(int id)
        {
            return await _context.Tags
                .FirstOrDefaultAsync(t => t.TagId == id);
        }

        public async Task<Tag?> GetByNameAsync(string name)
        {
            return await _context.Tags
                .FirstOrDefaultAsync(t => t.Name.ToLower() == name.ToLower());
        }

        public async Task<Tag> CreateAsync(Tag tag)
        {
            _context.Tags.Add(tag);
            await _context.SaveChangesAsync();
            return tag;
        }

        public async Task<IEnumerable<Tag>> GetOrCreateTagsAsync(List<string> tagNames)
        {
            var tags = new List<Tag>();
            var now = DateTime.UtcNow;

            foreach (var tagName in tagNames)
            {
                var normalizedName = tagName.Trim();
                var existingTag = await GetByNameAsync(normalizedName);

                if (existingTag != null)
                {
                    tags.Add(existingTag);
                }
                else
                {
                    var newTag = new Tag
                    {
                        Name = normalizedName,
                        CreatedAt = now,
                        UpdatedAt = now
                    };
                    var created = await CreateAsync(newTag);
                    tags.Add(created);
                }
            }

            return tags;
        }

        public async Task<IEnumerable<Tag>> SearchTagsAsync(string searchTerm)
        {
            if (string.IsNullOrWhiteSpace(searchTerm))
            {
                return await GetAllAsync();
            }

            return await _context.Tags
                .Where(t => t.Name.ToLower().Contains(searchTerm.ToLower()))
                .OrderBy(t => t.Name)
                .Take(20)
                .ToListAsync();
        }
    }
}

