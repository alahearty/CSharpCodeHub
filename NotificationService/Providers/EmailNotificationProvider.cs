namespace NotificationService.Providers;

using NotificationService.Models;

// Concrete implementation of INotificationProvider
public class EmailNotificationProvider : INotificationProvider
{
    public string Name => "Email";
    public bool IsAvailable => true;

    public string Send(NotificationMessage message)
    {
        if (message == null)
            throw new ArgumentNullException(nameof(message));

        // Simulate email sending
        Console.WriteLine($"📧 Sending email to {message.Recipient}: {message.Content}");
        
        // Simulate processing time
        Thread.Sleep(100);
        
        return $"Email sent successfully to {message.Recipient}";
    }

    public async Task<string> SendAsync(NotificationMessage message)
    {
        if (message == null)
            throw new ArgumentNullException(nameof(message));

        // Simulate async email sending
        await Task.Delay(100);
        
        Console.WriteLine($"📧 [Async] Sending email to {message.Recipient}: {message.Content}");
        
        return $"Email sent successfully to {message.Recipient} (async)";
    }
}
