using GPBackend.Models;
using GPBackend.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GPBackend.Repositories.Implements
{
    public class UserRepository : IUserRepository 
    {
        private readonly GPDBContext _context;
        
        public UserRepository(GPDBContext context)
        {
            _context = context;
        }
        
        public async Task<User?> GetByIdAsync(int id)
        {
            return await _context.Users
                .SingleOrDefaultAsync(u => u.UserId == id && !u.IsDeleted);
        }
        
        public async Task<User?> GetByEmailAsync(string email)
        {
            return await _context.Users
                .SingleOrDefaultAsync(u => u.Email == email && !u.IsDeleted);
        }
        
        public async Task<IEnumerable<User>> GetAllAsync()
        {
            return await _context.Users
                .Where(u => !u.IsDeleted)
                .ToListAsync();
        }
        
        public async Task<int> CreateAsync(User user)
        {
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            return user.UserId;
        }
        
        public async Task<bool> UpdateAsync(User user)
        {
            var existingUser = await _context.Users.FindAsync(user.UserId);
            if (existingUser == null || existingUser.IsDeleted)
                return false;
                
            // Update only non-sensitive fields
            existingUser.Fname = user.Fname;
            existingUser.Lname = user.Lname;
            existingUser.Email = user.Email;
            existingUser.Address = user.Address;
            existingUser.BirthDate = user.BirthDate;
            existingUser.UpdatedAt = DateTime.UtcNow;
            
            // Don't update password or security-related fields here
            
            try
            {
                await _context.SaveChangesAsync();
                return true;
            }
            catch (DbUpdateConcurrencyException)
            {
                return await UserExistsAsync(user.UserId);
            }
        }
        
        public async Task<bool> DeleteAsync(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null)
                return false;
                
            // Soft delete
            user.IsDeleted = true;
            user.UpdatedAt = DateTime.UtcNow;
            
            await _context.SaveChangesAsync();
            return true;
        }
        
        public async Task<bool> EmailExistsAsync(string email)
        {
            return await _context.Users
                .AnyAsync(u => u.Email == email && !u.IsDeleted);
        }
        
        public async Task<bool> ChangePasswordAsync(int id, string newPassword)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null || user.IsDeleted)
                return false;
                
            user.Password = newPassword; // Assume this is already hashed by the service
            user.UpdatedAt = DateTime.UtcNow;
            
            await _context.SaveChangesAsync();
            return true;
        }
        
        private async Task<bool> UserExistsAsync(int id)
        {
            return await _context.Users.AnyAsync(u => u.UserId == id && !u.IsDeleted);
        }
    }
}