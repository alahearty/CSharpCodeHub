namespace NotificationService.Services;

// Concrete command implementation
public class SendPushCommand : INotificationCommand
{
    private readonly string _deviceId;
    private readonly string _content;
    private bool _wasExecuted = false;

    public string Description => $"Send push notification to device {_deviceId}";

    public SendPushCommand(string deviceId, string content)
    {
        _deviceId = deviceId ?? throw new ArgumentNullException(nameof(deviceId));
        _content = content ?? throw new ArgumentNullException(nameof(content));
    }

    public void Execute()
    {
        if (!_wasExecuted)
        {
            Console.WriteLine($"  🔔 Executing: Sending push notification to device {_deviceId}");
            Console.WriteLine($"  🔔 Content: {_content}");
            
            // Simulate push notification sending
            Thread.Sleep(30);
            
            Console.WriteLine($"  ✅ Push notification sent successfully to device {_deviceId}");
            _wasExecuted = true;
        }
    }

    public void Undo()
    {
        if (_wasExecuted)
        {
            Console.WriteLine($"  ↩️ Undoing: Push notification sent to device {_deviceId}");
            Console.WriteLine($"  🔔 Sending cancellation push to device {_deviceId}");
            
            // Simulate cancellation
            Thread.Sleep(15);
            
            Console.WriteLine($"  ✅ Cancellation push sent to device {_deviceId}");
            _wasExecuted = false;
        }
    }
}
