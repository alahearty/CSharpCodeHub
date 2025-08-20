namespace NotificationService.Services;

using NotificationService.Models;

// Observer interface for the Observer pattern
public interface INotificationObserver
{
    void Update(NotificationMessage message);
    string ObserverName { get; }
}
