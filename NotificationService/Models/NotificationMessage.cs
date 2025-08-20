namespace NotificationService.Models;

public enum NotificationPriority
{
    Low,
    Normal,
    High,
    Critical
}

public class NotificationMessage
{
    public Guid Id { get; set; }
    public string Content { get; set; } = string.Empty;
    public NotificationPriority Priority { get; set; }
    public string Recipient { get; set; } = string.Empty;
    public DateTime Timestamp { get; set; }
    public Dictionary<string, object> Metadata { get; set; } = new();

    public override string ToString()
    {
        return $"[{Priority}] {Content} -> {Recipient} at {Timestamp:yyyy-MM-dd HH:mm:ss}";
    }
}
