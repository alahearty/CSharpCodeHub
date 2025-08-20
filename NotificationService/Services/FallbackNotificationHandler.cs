namespace NotificationService.Services;

using NotificationService.Models;

// Fallback handler for the Chain of Responsibility pattern
public class FallbackNotificationHandler : INotificationHandler
{
    public string HandlerName => "Fallback Handler";

    public void SetNext(INotificationHandler handler)
    {
        // Fallback handler is always the last in the chain
        // No next handler needed
    }

    public void Handle(NotificationMessage message)
    {
        Console.WriteLine($"  🆘 {HandlerName}: Processing message as fallback");
        Console.WriteLine($"  📝 {HandlerName}: Logging message for manual review");
        
        // Simulate fallback processing
        Thread.Sleep(20);
        
        Console.WriteLine($"  ✅ {HandlerName}: Message logged for manual review");
        Console.WriteLine($"  📋 {HandlerName}: Message details - Priority: {message.Priority}, Content: {message.Content}");
    }
}
