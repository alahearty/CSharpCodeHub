namespace NotificationService.Providers;

using NotificationService.Models;

// Interface demonstrating DEPENDENCY INVERSION PRINCIPLE
// High-level modules depend on this abstraction, not concrete implementations
public interface INotificationProvider
{
    string Name { get; }
    bool IsAvailable { get; }
    string Send(NotificationMessage message);
    Task<string> SendAsync(NotificationMessage message);
}
