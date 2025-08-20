namespace NotificationService.Services;

using NotificationService.Models;

// Concrete handler for SMS notifications
public class SmsNotificationHandler : INotificationHandler
{
    private INotificationHandler? _nextHandler;

    public string HandlerName => "SMS Handler";

    public void SetNext(INotificationHandler handler)
    {
        _nextHandler = handler;
    }

    public void Handle(NotificationMessage message)
    {
        if (message.Priority == NotificationPriority.Normal)
        {
            Console.WriteLine($"  📱 {HandlerName}: Processing normal priority SMS notification");
            
            // Simulate SMS processing
            Thread.Sleep(50);
            
            Console.WriteLine($"  ✅ {HandlerName}: Normal priority SMS sent successfully");
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
