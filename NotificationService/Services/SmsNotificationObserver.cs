namespace NotificationService.Services;

using NotificationService.Models;

// Concrete observer implementation
public class SmsNotificationObserver : INotificationObserver
{
    public string ObserverName => "SMS Observer";

    public void Update(NotificationMessage message)
    {
        Console.WriteLine($"  📱 {ObserverName}: Processing SMS notification for {message.Recipient}");
        
        // Simulate SMS processing
        Thread.Sleep(30);
        
        Console.WriteLine($"  📱 {ObserverName}: SMS notification processed successfully");
    }
}
