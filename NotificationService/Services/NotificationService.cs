namespace NotificationService.Services;

using NotificationService.Models;
using NotificationService.Providers;

// High-level service that depends on abstractions (INotificationProvider)
// Demonstrates DEPENDENCY INVERSION PRINCIPLE
public class NotificationService
{
    private readonly List<INotificationProvider> _providers = new();

    public void AddProvider(INotificationProvider provider)
    {
        if (provider == null)
            throw new ArgumentNullException(nameof(provider));
        
        _providers.Add(provider);
        Console.WriteLine($"✅ Added notification provider: {provider.Name}");
    }

    public void RemoveProvider(INotificationProvider provider)
    {
        if (provider != null && _providers.Remove(provider))
        {
            Console.WriteLine($"❌ Removed notification provider: {provider.Name}");
        }
    }

    public void SendNotification(NotificationMessage message)
    {
        if (message == null)
            throw new ArgumentNullException(nameof(message));

        Console.WriteLine($"\n📤 Sending notification: {message.Content}");
        
        foreach (var provider in _providers.Where(p => p.IsAvailable))
        {
            try
            {
                var result = provider.Send(message);
                Console.WriteLine($"  ✅ {provider.Name}: {result}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"  ❌ {provider.Name}: Failed - {ex.Message}");
            }
        }
    }

    public async Task SendNotificationAsync(NotificationMessage message)
    {
        if (message == null)
            throw new ArgumentNullException(nameof(message));

        Console.WriteLine($"\n📤 Sending notification asynchronously: {message.Content}");
        
        var tasks = _providers
            .Where(p => p.IsAvailable)
            .Select(async provider =>
            {
                try
                {
                    var result = await provider.SendAsync(message);
                    Console.WriteLine($"  ✅ {provider.Name}: {result}");
                    return result;
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"  ❌ {provider.Name}: Failed - {ex.Message}");
                    return $"Failed: {ex.Message}";
                }
            });

        await Task.WhenAll(tasks);
    }

    public IEnumerable<INotificationProvider> GetAvailableProviders()
    {
        return _providers.Where(p => p.IsAvailable);
    }
}
