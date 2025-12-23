using GPBackend.Models.Enums;

namespace GPBackend.DTOs.Reaction
{
    public class WhoReactedDto
    {
        public List<ReactionUserDto> Reactions { get; set; } = new List<ReactionUserDto>();
        public int TotalCount { get; set; }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public int TotalPages { get; set; }
    }

    public class ReactionUserDto
    {
        public int UserId { get; set; }
        public string UserName { get; set; } = null!;
        public string? ProfilePictureUrl { get; set; }
        public ReactionType ReactionType { get; set; }
        public string ReactionTypeName { get; set; } = null!;
        public DateTime ReactedAt { get; set; }
    }

    public class GroupedWhoReactedDto
    {
        public Dictionary<string, List<ReactionUserDto>> ReactionsByType { get; set; } = new Dictionary<string, List<ReactionUserDto>>();
        public int TotalCount { get; set; }
    }
}

