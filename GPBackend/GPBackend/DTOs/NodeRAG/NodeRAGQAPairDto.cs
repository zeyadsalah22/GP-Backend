using System.Text.Json.Serialization;

namespace GPBackend.DTOs.NodeRAG
{
    public class NodeRAGQAPairCreateDto
    {
        public string Question { get; set; } = null!;
        public string Answer { get; set; } = null!;
        public string QuestionId { get; set; } = null!;
        public string UserId { get; set; } = null!;
        public string? JobTitle { get; set; }
        public string? CompanyName { get; set; }
        public DateTime? SubmissionDate { get; set; }
    }

    public class NodeRAGQAPairResponseDto
    {
        [JsonPropertyName("success")]
        public bool Success { get; set; }

        [JsonPropertyName("message")]
        public string Message { get; set; } = null!;

        [JsonPropertyName("question_hash_id")]
        public string QuestionHashId { get; set; } = null!;

        [JsonPropertyName("answer_hash_id")]
        public string AnswerHashId { get; set; } = null!;

        [JsonPropertyName("question")]
        public string Question { get; set; } = null!;

        [JsonPropertyName("answer")]
        public string Answer { get; set; } = null!;

        [JsonPropertyName("added_to_graph")]
        public bool AddedToGraph { get; set; }
    }
}

