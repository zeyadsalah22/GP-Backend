namespace GPBackend.DTOs.Gmail;

/// <summary>
/// Gmail connection data for n8n workflow (includes access token)
/// </summary>
public class GmailConnectionForN8nDto
{
    public int UserId { get; set; }
    public string UserEmail { get; set; } = null!;
    public string GmailAddress { get; set; } = null!;
    public string AccessToken { get; set; } = null!;
    public string? HistoryId { get; set; }
}

