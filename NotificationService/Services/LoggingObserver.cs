namespace NotificationService.Services;

using NotificationService.Models;

// Concrete observer implementation for logging
public class LoggingObserver : INotificationObserver
{
    public string ObserverName => "Logging Observer";

    public void Update(NotificationMessage message)
    {
        Console.WriteLine($"  📝 {ObserverName}: Logging notification - ID: {message.Id}, Priority: {message.Priority}");
        
        // Simulate logging
        Thread.Sleep(10);
        
        Console.WriteLine($"  📝 {ObserverName}: Notification logged successfully");
    }
}
