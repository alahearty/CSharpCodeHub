namespace NotificationService.Services;

// Invoker class for the Command pattern
public class NotificationCommandInvoker
{
    private readonly Stack<INotificationCommand> _commandHistory = new();

    public void QueueCommand(INotificationCommand command)
    {
        if (command != null)
        {
            _commandHistory.Push(command);
            Console.WriteLine($"📋 Queued command: {command.Description}");
        }
    }

    public void ExecuteCommands()
    {
        var commandsToExecute = _commandHistory.ToList();
        _commandHistory.Clear();

        foreach (var command in commandsToExecute)
        {
            try
            {
                command.Execute();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"  ❌ Error executing command '{command.Description}': {ex.Message}");
            }
        }
    }

    public void UndoLastCommand()
    {
        if (_commandHistory.Count > 0)
        {
            var lastCommand = _commandHistory.Pop();
            try
            {
                lastCommand.Undo();
                Console.WriteLine($"  ↩️ Undid command: {lastCommand.Description}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"  ❌ Error undoing command '{lastCommand.Description}': {ex.Message}");
            }
        }
        else
        {
            Console.WriteLine("  ℹ️ No commands to undo");
        }
    }
}
