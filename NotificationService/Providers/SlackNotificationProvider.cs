namespace NotificationService.Providers;

using NotificationService.Models;

// New notification provider demonstrating LISKOV SUBSTITUTION PRINCIPLE
// Can be used anywhere INotificationProvider is expected
public class SlackNotificationProvider : INotificationProvider
{
    public string Name => "Slack";
    public bool IsAvailable => true;

    public string Send(NotificationMessage message)
    {
        if (message == null)
            throw new ArgumentNullException(nameof(message));

        // Simulate Slack message sending
        Console.WriteLine($"💬 Sending Slack message to {message.Recipient}: {message.Content}");
        
        // Simulate processing time
        Thread.Sleep(80);
        
        return $"Slack message sent successfully to {message.Recipient}";
    }

    public async Task<string> SendAsync(NotificationMessage message)
    {
        if (message == null)
            throw new ArgumentNullException(nameof(message));

        // Simulate async Slack message sending
        await Task.Delay(80);
        
        Console.WriteLine($"💬 [Async] Sending Slack message to {message.Recipient}: {message.Content}");
        
        return $"Slack message sent successfully to {message.Recipient} (async)";
    }
}
