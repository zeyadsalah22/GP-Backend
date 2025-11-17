using GPBackend.DTOs.Post;

namespace GPBackend.DTOs.SavedPost;

public class SavedPostResponseDto
{
    public int SavedPostId { get; set; }
    public int UserId { get; set; }
    public int PostId { get; set; }
    public DateTime SavedAt { get; set; }
    public PostResponseDto? Post { get; set; }
}

