namespace NotificationService.Services;

using NotificationService.Models;

// Concrete handler for push notifications
public class PushNotificationHandler : INotificationHandler
{
    private INotificationHandler? _nextHandler;

    public string HandlerName => "Push Handler";

    public void SetNext(INotificationHandler handler)
    {
        _nextHandler = handler;
    }

    public void Handle(NotificationMessage message)
    {
        if (message.Priority == NotificationPriority.Low)
        {
            Console.WriteLine($"  🔔 {HandlerName}: Processing low priority push notification");
            
            // Simulate push notification processing
            Thread.Sleep(30);
            
            Console.WriteLine($"  ✅ {HandlerName}: Low priority push notification sent successfully");
        }
        else if (_nextHandler != null)
        {
            Console.WriteLine($"  ⏭️ {HandlerName}: Passing to next handler");
            _nextHandler.Handle(message);
        }
        else
        {
            Console.WriteLine($"  ❌ {HandlerName}: No handler available for this message");
        }
    }
}
