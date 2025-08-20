namespace NotificationService.Services;

// Concrete command implementation
public class SendEmailCommand : INotificationCommand
{
    private readonly string _recipient;
    private readonly string _content;
    private bool _wasExecuted = false;

    public string Description => $"Send email to {_recipient}";

    public SendEmailCommand(string recipient, string content)
    {
        _recipient = recipient ?? throw new ArgumentNullException(nameof(recipient));
        _content = content ?? throw new ArgumentNullException(nameof(content));
    }

    public void Execute()
    {
        if (!_wasExecuted)
        {
            Console.WriteLine($"  📧 Executing: Sending email to {_recipient}");
            Console.WriteLine($"  📧 Content: {_content}");
            
            // Simulate email sending
            Thread.Sleep(100);
            
            Console.WriteLine($"  ✅ Email sent successfully to {_recipient}");
            _wasExecuted = true;
        }
    }

    public void Undo()
    {
        if (_wasExecuted)
        {
            Console.WriteLine($"  ↩️ Undoing: Email sent to {_recipient}");
            Console.WriteLine($"  📧 Sending cancellation email to {_recipient}");
            
            // Simulate cancellation
            Thread.Sleep(50);
            
            Console.WriteLine($"  ✅ Cancellation email sent to {_recipient}");
            _wasExecuted = false;
        }
    }
}
