using System.Text.Json.Serialization;

namespace GPBackend.DTOs.Gmail;

/// <summary>
/// Represents a Pub/Sub push notification from Google Cloud
/// </summary>
public class PubSubNotificationDto
{
    [JsonPropertyName("message")]
    public PubSubMessageDto Message { get; set; } = null!;

    [JsonPropertyName("subscription")]
    public string Subscription { get; set; } = null!;
}

/// <summary>
/// The message payload from Pub/Sub
/// </summary>
public class PubSubMessageDto
{
    /// <summary>
    /// Base64-encoded Gmail notification data
    /// </summary>
    [JsonPropertyName("data")]
    public string Data { get; set; } = null!;

    [JsonPropertyName("messageId")]
    public string MessageId { get; set; } = null!;

    [JsonPropertyName("publishTime")]
    public string PublishTime { get; set; } = null!;
}

/// <summary>
/// Gmail notification data (decoded from base64)
/// </summary>
public class GmailNotificationDataDto
{
    [JsonPropertyName("emailAddress")]
    public string EmailAddress { get; set; } = null!;

    [JsonPropertyName("historyId")]
    public string HistoryId { get; set; } = null!;
}

