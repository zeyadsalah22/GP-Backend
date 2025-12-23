namespace GPBackend.DTOs.Gmail
{
    /// <summary>
    /// DTO for returning Gmail connection status to frontend
    /// </summary>
    public class GmailConnectionResponseDto
    {
        public int GmailConnectionId { get; set; }
        public int UserId { get; set; }
        public string GmailAddress { get; set; } = null!;
        public bool IsActive { get; set; }
        public DateTime ConnectedAt { get; set; }
        public DateTime? LastCheckedAt { get; set; }
        public bool IsTokenExpired { get; set; }
    }
}

