namespace GPBackend.DTOs.Resume
{
    public class ResumeResponseDto
    {
        public int ResumeId { get; set; }

        public int UserId { get; set; }

        public byte[] ResumeFile { get; set; } = null!;

        public DateTime CreatedAt { get; set; }

        public DateTime UpdatedAt { get; set; }
    }
}
