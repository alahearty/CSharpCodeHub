namespace NotificationService.Services;

using NotificationService.Models;

// Concrete observer implementation
public class EmailNotificationObserver : INotificationObserver
{
    public string ObserverName => "Email Observer";

    public void Update(NotificationMessage message)
    {
        Console.WriteLine($"  📧 {ObserverName}: Processing email notification for {message.Recipient}");
        
        // Simulate email processing
        Thread.Sleep(50);
        
        Console.WriteLine($"  📧 {ObserverName}: Email notification processed successfully");
    }
}
