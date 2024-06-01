using System;
using PasswordManagerAPI.Models;

public class PasswordEntry
{
    public string Name { get; set; } = string.Empty;
    public string Site { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; }
    public User? User { get; set; }
}
