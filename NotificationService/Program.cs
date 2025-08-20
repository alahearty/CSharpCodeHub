using NotificationService.Models;
using NotificationService.Services;
using NotificationService.Factories;
using NotificationService.Providers;

namespace NotificationService;

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("📱 Advanced OOP & SOLID Principles - Notification Service Tutorial");
        Console.WriteLine("================================================================\n");

        // Demonstrate Dependency Inversion Principle
        DemonstrateDependencyInversionPrinciple();
        
        Console.WriteLine("\n" + new string('-', 60) + "\n");
        
        // Demonstrate Liskov Substitution Principle
        DemonstrateLiskovSubstitutionPrinciple();
        
        Console.WriteLine("\n" + new string('-', 60) + "\n");
        
        // Demonstrate Observer Pattern
        DemonstrateObserverPattern();
        
        Console.WriteLine("\n" + new string('-', 60) + "\n");
        
        // Demonstrate Command Pattern
        DemonstrateCommandPattern();
        
        Console.WriteLine("\n" + new string('-', 60) + "\n");
        
        // Demonstrate Chain of Responsibility Pattern
        DemonstrateChainOfResponsibilityPattern();
        
        Console.WriteLine("\nPress any key to exit...");
        Console.ReadKey();
    }

    static void DemonstrateDependencyInversionPrinciple()
    {
        Console.WriteLine("🔹 DEPENDENCY INVERSION PRINCIPLE DEMONSTRATION");
        Console.WriteLine("=============================================");
        
        // High-level modules depend on abstractions, not concrete implementations
        var notificationService = new NotificationService();
        
        // Inject different notification providers (abstractions)
        var emailProvider = new EmailNotificationProvider();
        var smsProvider = new SmsNotificationProvider();
        var pushProvider = new PushNotificationProvider();
        
        // Service works with any INotificationProvider implementation
        notificationService.AddProvider(emailProvider);
        notificationService.AddProvider(smsProvider);
        notificationService.AddProvider(pushProvider);
        
        var message = new NotificationMessage
        {
            Id = Guid.NewGuid(),
            Content = "Hello from DIP demonstration!",
            Priority = NotificationPriority.Normal,
            Recipient = "user@example.com",
            Timestamp = DateTime.Now
        };
        
        Console.WriteLine("\nSending notification through all providers:");
        notificationService.SendNotification(message);
    }

    static void DemonstrateLiskovSubstitutionPrinciple()
    {
        Console.WriteLine("🔹 LISKOV SUBSTITUTION PRINCIPLE DEMONSTRATION");
        Console.WriteLine("==============================================");
        
        // Base type can be substituted with derived types
        var notificationProviders = new List<INotificationProvider>
        {
            new EmailNotificationProvider(),
            new SmsNotificationProvider(),
            new PushNotificationProvider(),
            new SlackNotificationProvider(), // New provider type
            new DiscordNotificationProvider() // Another new provider type
        };
        
        var message = new NotificationMessage
        {
            Id = Guid.NewGuid(),
            Content = "LSP test message",
            Priority = NotificationPriority.High,
            Recipient = "test@example.com",
            Timestamp = DateTime.Now
        };
        
        Console.WriteLine("\nTesting LSP with different provider types:");
        foreach (var provider in notificationProviders)
        {
            try
            {
                var result = provider.Send(message);
                Console.WriteLine($"  {provider.GetType().Name}: {result}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"  {provider.GetType().Name}: Failed - {ex.Message}");
            }
        }
    }

    static void DemonstrateObserverPattern()
    {
        Console.WriteLine("🔹 OBSERVER PATTERN DEMONSTRATION");
        Console.WriteLine("=================================");
        
        var notificationCenter = new NotificationCenter();
        
        // Create observers
        var emailObserver = new EmailNotificationObserver();
        var smsObserver = new SmsNotificationObserver();
        var logObserver = new LoggingObserver();
        
        // Subscribe observers
        notificationCenter.Subscribe(emailObserver);
        notificationCenter.Subscribe(smsObserver);
        notificationCenter.Subscribe(logObserver);
        
        var message = new NotificationMessage
        {
            Id = Guid.NewGuid(),
            Content = "Observer pattern test",
            Priority = NotificationPriority.Normal,
            Recipient = "observer@test.com",
            Timestamp = DateTime.Now
        };
        
        Console.WriteLine("\nSending notification - all observers will be notified:");
        notificationCenter.Notify(message);
        
        // Unsubscribe one observer
        notificationCenter.Unsubscribe(smsObserver);
        
        Console.WriteLine("\nSending another notification (SMS observer unsubscribed):");
        var message2 = new NotificationMessage
        {
            Id = Guid.NewGuid(),
            Content = "Second observer test",
            Priority = NotificationPriority.High,
            Recipient = "observer2@test.com",
            Timestamp = DateTime.Now
        };
        notificationCenter.Notify(message2);
    }

    static void DemonstrateCommandPattern()
    {
        Console.WriteLine("🔹 COMMAND PATTERN DEMONSTRATION");
        Console.WriteLine("=================================");
        
        var commandInvoker = new NotificationCommandInvoker();
        
        // Create commands
        var emailCommand = new SendEmailCommand("user@example.com", "Command pattern test");
        var smsCommand = new SendSmsCommand("+1234567890", "SMS via command");
        var pushCommand = new SendPushCommand("device123", "Push notification");
        
        // Queue commands
        commandInvoker.QueueCommand(emailCommand);
        commandInvoker.QueueCommand(smsCommand);
        commandInvoker.QueueCommand(pushCommand);
        
        Console.WriteLine("\nExecuting queued commands:");
        commandInvoker.ExecuteCommands();
        
        // Undo last command
        Console.WriteLine("\nUndoing last command:");
        commandInvoker.UndoLastCommand();
    }

    static void DemonstrateChainOfResponsibilityPattern()
    {
        Console.WriteLine("🔹 CHAIN OF RESPONSIBILITY PATTERN DEMONSTRATION");
        Console.WriteLine("===============================================");
        
        // Create the chain
        var emailHandler = new EmailNotificationHandler();
        var smsHandler = new SmsNotificationHandler();
        var pushHandler = new PushNotificationHandler();
        var fallbackHandler = new FallbackNotificationHandler();
        
        // Set up the chain
        emailHandler.SetNext(smsHandler);
        smsHandler.SetNext(pushHandler);
        pushHandler.SetNext(fallbackHandler);
        
        var messages = new List<NotificationMessage>
        {
            new NotificationMessage
            {
                Id = Guid.NewGuid(),
                Content = "High priority message",
                Priority = NotificationPriority.High,
                Recipient = "high@priority.com",
                Timestamp = DateTime.Now
            },
            new NotificationMessage
            {
                Id = Guid.NewGuid(),
                Content = "Normal priority message",
                Priority = NotificationPriority.Normal,
                Recipient = "normal@priority.com",
                Timestamp = DateTime.Now
            },
            new NotificationMessage
            {
                Id = Guid.NewGuid(),
                Content = "Low priority message",
                Priority = NotificationPriority.Low,
                Recipient = "low@priority.com",
                Timestamp = DateTime.Now
            }
        };
        
        Console.WriteLine("\nProcessing messages through the chain:");
        foreach (var message in messages)
        {
            Console.WriteLine($"\nProcessing: {message.Content} (Priority: {message.Priority})");
            emailHandler.Handle(message);
        }
    }
}
