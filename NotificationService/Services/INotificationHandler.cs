namespace NotificationService.Services;

using NotificationService.Models;

// Handler interface for the Chain of Responsibility pattern
public interface INotificationHandler
{
    void SetNext(INotificationHandler handler);
    void Handle(NotificationMessage message);
    string HandlerName { get; }
}
