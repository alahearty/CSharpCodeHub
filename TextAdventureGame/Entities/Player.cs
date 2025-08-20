namespace TextAdventureGame.Entities;

using TextAdventureGame.Core;

// Player class demonstrating INHERITANCE and ENCAPSULATION
public class Player : IGameObject
{
    public string Id { get; }
    public string Name { get; }
    public string Description { get; }
    public bool IsActive { get; set; }
    
    // Private fields with encapsulation
    private int _health;
    private int _maxHealth;
    private int _inventoryCapacity;
    private readonly List<IGameObject> _inventory;
    private ILocation _currentLocation;

    public int Health => _health;
    public int MaxHealth => _maxHealth;
    public int InventoryCapacity => _inventoryCapacity;
    public IReadOnlyList<IGameObject> Inventory => _inventory.AsReadOnly();
    public ILocation CurrentLocation => _currentLocation;

    public Player(string name)
    {
        Id = Guid.NewGuid().ToString("N")[..8];
        Name = name;
        Description = $"A brave adventurer named {name}";
        IsActive = true;
        _health = 100;
        _maxHealth = 100;
        _inventoryCapacity = 10;
        _inventory = new List<IGameObject>();
    }

    public void MoveTo(ILocation location)
    {
        if (location != null && location.IsActive)
        {
            _currentLocation = location;
            Console.WriteLine($"🏃 {Name} moved to {location.Name}");
        }
    }

    public void TakeDamage(int damage)
    {
        if (damage > 0)
        {
            _health = Math.Max(0, _health - damage);
            Console.WriteLine($"💥 {Name} took {damage} damage! Health: {_health}/{_maxHealth}");
            
            if (_health <= 0)
            {
                Console.WriteLine($"💀 {Name} has fallen!");
                IsActive = false;
            }
        }
    }

    public void Heal(int amount)
    {
        if (amount > 0)
        {
            _health = Math.Min(_maxHealth, _health + amount);
            Console.WriteLine($"💚 {Name} healed {amount} health. Health: {_health}/{_maxHealth}");
        }
    }

    public bool AddToInventory(IGameObject item)
    {
        if (item != null && _inventory.Count < _inventoryCapacity)
        {
            _inventory.Add(item);
            Console.WriteLine($"📦 {Name} picked up {item.Name}");
            return true;
        }
        Console.WriteLine($"❌ {Name} cannot carry {item?.Name} - inventory full!");
        return false;
    }

    public void RemoveFromInventory(IGameObject item)
    {
        if (item != null && _inventory.Remove(item))
        {
            Console.WriteLine($"📤 {Name} dropped {item.Name}");
        }
    }

    public void ShowInventory()
    {
        Console.WriteLine($"\n🎒 {Name}'s Inventory ({_inventory.Count}/{_inventoryCapacity}):");
        if (_inventory.Count == 0)
        {
            Console.WriteLine("  Empty");
        }
        else
        {
            foreach (var item in _inventory)
            {
                Console.WriteLine($"  - {item.Name}: {item.Description}");
            }
        }
    }
}
