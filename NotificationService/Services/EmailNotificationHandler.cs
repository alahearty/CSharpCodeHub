namespace NotificationService.Services;

using NotificationService.Models;

// Concrete handler for email notifications
public class EmailNotificationHandler : INotificationHandler
{
    private INotificationHandler? _nextHandler;

    public string HandlerName => "Email Handler";

    public void SetNext(INotificationHandler handler)
    {
        _nextHandler = handler;
    }

    public void Handle(NotificationMessage message)
    {
        if (message.Priority == NotificationPriority.High)
        {
            Console.WriteLine($"  📧 {HandlerName}: Processing high priority email notification");
            
            // Simulate email processing
            Thread.Sleep(100);
            
            Console.WriteLine($"  ✅ {HandlerName}: High priority email sent successfully");
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
