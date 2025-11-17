using System;
using System.Collections.Generic;
using GPBackend.Models.Enums;

namespace GPBackend.Models;

public partial class Post
{
    public int PostId { get; set; }

    public int UserId { get; set; }

    public PostType PostType { get; set; }

    public string? Title { get; set; }

    public string Content { get; set; } = null!;

    public bool IsAnonymous { get; set; }

    public PostStatus Status { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public bool IsDeleted { get; set; }

    public int CommentCount { get; set; }

    public byte[] Rowversion { get; set; } = null!;

    public virtual User User { get; set; } = null!;

    public virtual ICollection<PostTag> PostTags { get; set; } = new List<PostTag>();

    public virtual ICollection<Comment> Comments { get; set; } = new List<Comment>();

    public virtual ICollection<PostReaction> PostReactions { get; set; } = new List<PostReaction>();

    public virtual ICollection<SavedPost> SavedPosts { get; set; } = new List<SavedPost>();
}

