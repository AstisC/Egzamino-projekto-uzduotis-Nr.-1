using System;

namespace PasswordManagerAPI.Models
{
    public class PasswordEntry
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string? SiteName { get; set; } // Nullable
        public string? Password { get; set; } // Nullable
        public DateTime CreatedAt { get; set; }
        public User? User { get; set; } // Nullable
    }
}
