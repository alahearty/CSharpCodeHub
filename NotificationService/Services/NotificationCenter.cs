namespace NotificationService.Services;

using NotificationService.Models;

// Subject class for the Observer pattern
public class NotificationCenter
{
    private readonly List<INotificationObserver> _observers = new();

    public void Subscribe(INotificationObserver observer)
    {
        if (observer != null && !_observers.Contains(observer))
        {
            _observers.Add(observer);
            Console.WriteLine($"👥 {observer.ObserverName} subscribed to notifications");
        }
    }

    public void Unsubscribe(INotificationObserver observer)
    {
        if (observer != null && _observers.Remove(observer))
        {
            Console.WriteLine($"👋 {observer.ObserverName} unsubscribed from notifications");
        }
    }

    public void Notify(NotificationMessage message)
    {
        if (message == null)
            return;

        Console.WriteLine($"📢 Notifying {_observers.Count} observers about: {message.Content}");
        
        foreach (var observer in _observers)
        {
            try
            {
                observer.Update(message);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"  ❌ Error notifying {observer.ObserverName}: {ex.Message}");
            }
        }
    }
}
