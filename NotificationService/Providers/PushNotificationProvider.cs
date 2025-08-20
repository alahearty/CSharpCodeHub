namespace NotificationService.Providers;

using NotificationService.Models;

// Concrete implementation of INotificationProvider
public class PushNotificationProvider : INotificationProvider
{
    public string Name => "Push";
    public bool IsAvailable => true;

    public string Send(NotificationMessage message)
    {
        if (message == null)
            throw new ArgumentNullException(nameof(message));

        // Simulate push notification sending
        Console.WriteLine($"🔔 Sending push notification to {message.Recipient}: {message.Content}");
        
        // Simulate processing time
        Thread.Sleep(30);
        
        return $"Push notification sent successfully to {message.Recipient}";
    }

    public async Task<string> SendAsync(NotificationMessage message)
    {
        if (message == null)
            throw new ArgumentNullException(nameof(message));

        // Simulate async push notification sending
        await Task.Delay(30);
        
        Console.WriteLine($"🔔 [Async] Sending push notification to {message.Recipient}: {message.Content}");
        
        return $"Push notification sent successfully to {message.Recipient} (async)";
    }
}
