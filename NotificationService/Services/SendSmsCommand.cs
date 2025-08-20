namespace NotificationService.Services;

// Concrete command implementation
public class SendSmsCommand : INotificationCommand
{
    private readonly string _phoneNumber;
    private readonly string _content;
    private bool _wasExecuted = false;

    public string Description => $"Send SMS to {_phoneNumber}";

    public SendSmsCommand(string phoneNumber, string content)
    {
        _phoneNumber = phoneNumber ?? throw new ArgumentNullException(nameof(phoneNumber));
        _content = content ?? throw new ArgumentNullException(nameof(content));
    }

    public void Execute()
    {
        if (!_wasExecuted)
        {
            Console.WriteLine($"  📱 Executing: Sending SMS to {_phoneNumber}");
            Console.WriteLine($"  📱 Content: {_content}");
            
            // Simulate SMS sending
            Thread.Sleep(50);
            
            Console.WriteLine($"  ✅ SMS sent successfully to {_phoneNumber}");
            _wasExecuted = true;
        }
    }

    public void Undo()
    {
        if (_wasExecuted)
        {
            Console.WriteLine($"  ↩️ Undoing: SMS sent to {_phoneNumber}");
            Console.WriteLine($"  📱 Sending cancellation SMS to {_phoneNumber}");
            
            // Simulate cancellation
            Thread.Sleep(25);
            
            Console.WriteLine($"  ✅ Cancellation SMS sent to {_phoneNumber}");
            _wasExecuted = false;
        }
    }
}
