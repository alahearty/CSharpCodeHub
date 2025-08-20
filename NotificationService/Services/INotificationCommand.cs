namespace NotificationService.Services;

using NotificationService.Models;

// Command interface for the Command pattern
public interface INotificationCommand
{
    void Execute();
    void Undo();
    string Description { get; }
}
