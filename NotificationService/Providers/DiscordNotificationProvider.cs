namespace NotificationService.Providers;

using NotificationService.Models;

// Another new notification provider demonstrating LISKOV SUBSTITUTION PRINCIPLE
public class DiscordNotificationProvider : INotificationProvider
{
    public string Name => "Discord";
    public bool IsAvailable => true;

    public string Send(NotificationMessage message)
    {
        if (message == null)
            throw new ArgumentNullException(nameof(message));

        // Simulate Discord message sending
        Console.WriteLine($"🎮 Sending Discord message to {message.Recipient}: {message.Content}");
        
        // Simulate processing time
        Thread.Sleep(60);
        
        return $"Discord message sent successfully to {message.Recipient}";
    }

    public async Task<string> SendAsync(NotificationMessage message)
    {
        if (message == null)
            throw new ArgumentNullException(nameof(message));

        // Simulate async Discord message sending
        await Task.Delay(60);
        
        Console.WriteLine($"🎮 [Async] Sending Discord message to {message.Recipient}: {message.Content}");
        
        return $"Discord message sent successfully to {message.Recipient} (async)";
    }
}
