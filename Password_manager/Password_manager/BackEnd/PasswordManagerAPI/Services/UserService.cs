using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PasswordManagerAPI.Data;
using PasswordManagerAPI.Models;

namespace PasswordManagerAPI.Services
{
    public class UserService
    {
        private readonly ApplicationDbContext _context;
        private const int SaltSize = 10;

        public UserService(ApplicationDbContext context)
        {
            _context = context;
        }

        public string GenerateSalt()
        {
            var saltBytes = new byte[SaltSize];
            RandomNumberGenerator.Fill(saltBytes);
            return Convert.ToBase64String(saltBytes);
        }

        public string HashPassword(string password, string salt)
        {
            if (string.IsNullOrEmpty(password)) throw new ArgumentNullException(nameof(password));
            if (string.IsNullOrEmpty(salt)) throw new ArgumentNullException(nameof(salt));

            using var sha256 = SHA256.Create();
            var saltedPassword = password + salt;
            var saltedPasswordBytes = Encoding.UTF8.GetBytes(saltedPassword);
            var hashBytes = sha256.ComputeHash(saltedPasswordBytes);
            return Convert.ToBase64String(hashBytes);
        }

        public async Task<ServiceResult> RegisterUserAsync(UserRegisterModel model)
        {
            var existingUser = await _context.Users.SingleOrDefaultAsync(u => u.Username == model.Username);
            if (existingUser != null)
            {
                return new ServiceResult { Success = false, Message = "User already exists" };
            }

            var salt = GenerateSalt();
            var hashedPassword = HashPassword(model.Password, salt);

            var user = new User
            {
                Username = model.Username,
                PasswordHash = hashedPassword,
                Salt = salt
            };

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return new ServiceResult { Success = true, Message = "User registered successfully" };
        }

        public async Task<ServiceResult> LoginUserAsync(UserLoginModel model)
        {
            var user = await _context.Users.SingleOrDefaultAsync(u => u.Username == model.Username);
            if (user == null)
            {
                return new ServiceResult { Success = false, Message = "Invalid username or password" };
            }

            var hashedPassword = HashPassword(model.Password, user.Salt);
            if (hashedPassword != user.PasswordHash)
            {
                return new ServiceResult { Success = false, Message = "Invalid username or password" };
            }

            return new ServiceResult { Success = true, Message = "Login successful" };
        }

        public async Task<ServiceResult> AddPasswordEntryAsync(int userId, string siteName, string password)
        {
            var passwordEntry = new PasswordEntry
            {
                UserId = userId,
                SiteName = siteName,
                Password = password,
                CreatedAt = DateTime.UtcNow
            };

            _context.PasswordEntries.Add(passwordEntry);
            await _context.SaveChangesAsync();

            return new ServiceResult { Success = true, Message = "Password entry added successfully" };
        }

        public async Task<ServiceResult> UpdatePasswordEntryAsync(int entryId, string newPassword)
        {
            var entry = await _context.PasswordEntries.FindAsync(entryId);
            if (entry == null)
            {
                return new ServiceResult { Success = false, Message = "Password entry not found" };
            }

            entry.Password = newPassword;
            entry.CreatedAt = DateTime.UtcNow;

            _context.PasswordEntries.Update(entry);
            await _context.SaveChangesAsync();

            return new ServiceResult { Success = true, Message = "Password entry updated successfully" };
        }

        public async Task<ServiceResult> DeletePasswordEntryAsync(int entryId)
        {
            var entry = await _context.PasswordEntries.FindAsync(entryId);
            if (entry == null)
            {
                return new ServiceResult { Success = false, Message = "Password entry not found" };
            }

            _context.PasswordEntries.Remove(entry);
            await _context.SaveChangesAsync();

            return new ServiceResult { Success = true, Message = "Password entry deleted successfully" };
        }

        public async Task<PasswordEntry?> GetPasswordEntryAsync(int entryId)
        {
            return await _context.PasswordEntries.FindAsync(entryId);
        }

        public async Task<List<PasswordEntry>> GetUserPasswordEntriesAsync(int userId)
        {
            return await _context.PasswordEntries
                .Where(entry => entry.UserId == userId)
                .ToListAsync();
        }
    }
}