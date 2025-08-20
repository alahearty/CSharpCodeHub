namespace TextAdventureGame.Commands;

using TextAdventureGame.Core;
using TextAdventureGame.Entities;

// Concrete command implementation
public class TakeCommand : ICommand
{
    public string Name => "take";
    public string Description => "Take an item from the current location";

    public bool CanExecute(Player player)
    {
        return player != null && player.IsActive && player.CurrentLocation != null;
    }

    public void Execute(Player player, string[] parameters)
    {
        if (!CanExecute(player))
        {
            Console.WriteLine("❌ Cannot take items right now.");
            return;
        }

        if (parameters.Length == 0)
        {
            Console.WriteLine("❌ Please specify what to take.");
            ShowAvailableItems(player);
            return;
        }

        var itemName = string.Join(" ", parameters).ToLower();
        var currentLocation = player.CurrentLocation;
        
        var item = currentLocation.Objects
            .FirstOrDefault(obj => obj.IsActive && obj.Name.ToLower().Contains(itemName));

        if (item != null)
        {
            if (player.AddToInventory(item))
            {
                currentLocation.RemoveObject(item);
            }
        }
        else
        {
            Console.WriteLine($"❌ Cannot find '{string.Join(" ", parameters)}' here.");
            ShowAvailableItems(player);
        }
    }

    private void ShowAvailableItems(Player player)
    {
        var location = player.CurrentLocation;
        var items = location.Objects.Where(obj => obj.IsActive).ToList();
        
        if (items.Count == 0)
        {
            Console.WriteLine("📭 No items available to take here.");
        }
        else
        {
            Console.WriteLine("📦 Items available to take:");
            foreach (var item in items)
            {
                Console.WriteLine($"  - {item.Name}: {item.Description}");
            }
        }
    }
}
