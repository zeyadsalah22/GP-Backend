namespace GPBackend.DTOs.Chatbot
{
    public class SendMessageRequestDto
    {
        public string SessionId { get; set; }
        public string Message { get; set; }
    }
}