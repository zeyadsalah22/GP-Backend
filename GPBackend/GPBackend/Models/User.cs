using System;
using System.Collections.Generic;
using GPBackend.Models.Enums;

namespace GPBackend.Models;

public partial class User
{
    public int UserId { get; set; }

    public string Fname { get; set; } = null!;

    public string Lname { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string Password { get; set; } = null!;

    public string? Address { get; set; }

    public DateOnly? BirthDate { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public bool IsDeleted { get; set; }

    public byte[] Rowversion { get; set; } = null!;

    public UserRole Role { get; set; } = UserRole.User;

    public virtual ICollection<Interview> Interviews { get; set; } = new List<Interview>();

    public virtual ICollection<Resume> Resumes { get; set; } = new List<Resume>();

    public virtual ICollection<TodoList> TodoLists { get; set; } = new List<TodoList>();

    public virtual ICollection<UserCompany> UserCompanies { get; set; } = new List<UserCompany>();

    public virtual ICollection<RefreshToken> RefreshTokens { get; set; } = new List<RefreshToken>();
}
