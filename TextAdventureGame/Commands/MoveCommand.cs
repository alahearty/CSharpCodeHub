namespace TextAdventureGame.Commands;

using TextAdventureGame.Core;
using TextAdventureGame.Entities;

// Concrete command implementation
public class MoveCommand : ICommand
{
    public string Name => "move";
    public string Description => "Move to a connected location";

    public bool CanExecute(Player player)
    {
        return player != null && player.IsActive && player.CurrentLocation != null;
    }

    public void Execute(Player player, string[] parameters)
    {
        if (!CanExecute(player))
        {
            Console.WriteLine("❌ Cannot move right now.");
            return;
        }

        if (parameters.Length == 0)
        {
            Console.WriteLine("❌ Please specify where to move.");
            ShowAvailableLocations(player);
            return;
        }

        var targetLocationName = string.Join(" ", parameters).ToLower();
        var currentLocation = player.CurrentLocation;
        
        var targetLocation = currentLocation.ConnectedLocations
            .FirstOrDefault(loc => loc.Name.ToLower().Contains(targetLocationName));

        if (targetLocation != null)
        {
            player.MoveTo(targetLocation);
            ShowLocationDescription(player);
        }
        else
        {
            Console.WriteLine($"❌ Cannot move to '{string.Join(" ", parameters)}'.");
            ShowAvailableLocations(player);
        }
    }

    private void ShowAvailableLocations(Player player)
    {
        var currentLocation = player.CurrentLocation;
        Console.WriteLine($"\n📍 Available locations from {currentLocation.Name}:");
        
        foreach (var location in currentLocation.ConnectedLocations)
        {
            Console.WriteLine($"  - {location.Name}");
        }
    }

    private void ShowLocationDescription(Player player)
    {
        var location = player.CurrentLocation;
        Console.WriteLine($"\n🏞️  {location.Name}");
        Console.WriteLine($"   {location.Description}");
        
        if (location.Objects.Count > 0)
        {
            Console.WriteLine("\n📦 Objects here:");
            foreach (var obj in location.Objects.Where(o => o.IsActive))
            {
                Console.WriteLine($"  - {obj.Name}: {obj.Description}");
            }
        }
    }
}
