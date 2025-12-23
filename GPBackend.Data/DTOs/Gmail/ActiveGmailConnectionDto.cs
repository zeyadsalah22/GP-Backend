namespace GPBackend.DTOs.Gmail
{
    /// <summary>
    /// DTO for n8n to get list of active Gmail connections with tokens
    /// </summary>
    public class ActiveGmailConnectionDto
    {
        public int UserId { get; set; }
        public string UserEmail { get; set; } = null!;
        public string GmailAddress { get; set; } = null!;
        public string AccessToken { get; set; } = null!;  // Decrypted token for n8n
        public string LastCheckedAt { get; set; } = null!;  // ISO 8601 format
        public string CredentialId { get; set; } = null!;  // For n8n Gmail node
    }
}

