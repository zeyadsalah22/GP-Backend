using System.ComponentModel.DataAnnotations;

namespace GPBackend.DTOs.Gmail;

/// <summary>
/// DTO for updating Gmail history ID after processing push notification
/// </summary>
public class UpdateHistoryIdDto
{
    /// <summary>
    /// User ID whose history ID needs to be updated
    /// </summary>
    [Required]
    public int UserId { get; set; }

    /// <summary>
    /// New history ID from Gmail API
    /// </summary>
    [Required]
    [StringLength(50)]
    public string HistoryId { get; set; } = null!;
}

