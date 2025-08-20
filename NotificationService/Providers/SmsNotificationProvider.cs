namespace NotificationService.Providers;

using NotificationService.Models;

// Concrete implementation of INotificationProvider
public class SmsNotificationProvider : INotificationProvider
{
    public string Name => "SMS";
    public bool IsAvailable => true;

    public string Send(NotificationMessage message)
    {
        if (message == null)
            throw new ArgumentNullException(nameof(message));

        // Simulate SMS sending
        Console.WriteLine($"📱 Sending SMS to {message.Recipient}: {message.Content}");
        
        // Simulate processing time
        Thread.Sleep(50);
        
        return $"SMS sent successfully to {message.Recipient}";
    }

    public async Task<string> SendAsync(NotificationMessage message)
    {
        if (message == null)
            throw new ArgumentNullException(nameof(message));

        // Simulate async SMS sending
        await Task.Delay(50);
        
        Console.WriteLine($"📱 [Async] Sending SMS to {message.Recipient}: {message.Content}");
        
        return $"SMS sent successfully to {message.Recipient} (async)";
    }
}
