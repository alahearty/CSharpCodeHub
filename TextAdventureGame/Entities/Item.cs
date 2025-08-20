namespace TextAdventureGame.Entities;

using TextAdventureGame.Core;

// Base item class demonstrating ABSTRACTION
public abstract class Item : IGameObject
{
    public string Id { get; }
    public string Name { get; }
    public string Description { get; }
    public bool IsActive { get; set; }
    
    protected int _weight;
    protected int _value;

    public int Weight => _weight;
    public int Value => _value;

    protected Item(string name, string description, int weight, int value)
    {
        Id = Guid.NewGuid().ToString("N")[..8];
        Name = name;
        Description = description;
        IsActive = true;
        _weight = weight;
        _value = value;
    }

    // Abstract method that must be implemented by derived classes
    public abstract void Use(Player player);
    
    // Virtual method that can be overridden
    public virtual string GetDetailedDescription()
    {
        return $"{Description} (Weight: {_weight}, Value: {_value})";
    }
}

// Concrete item implementations demonstrating INHERITANCE and POLYMORPHISM
public class HealthPotion : Item
{
    private readonly int _healAmount;

    public HealthPotion() : base("Health Potion", "A magical potion that restores health", 1, 25)
    {
        _healAmount = 50;
    }

    public override void Use(Player player)
    {
        if (player != null)
        {
            player.Heal(_healAmount);
            Console.WriteLine($"🧪 {player.Name} used a Health Potion and restored {_healAmount} health!");
            IsActive = false; // Consumed
        }
    }

    public override string GetDetailedDescription()
    {
        return $"{base.GetDetailedDescription()} - Heals {_healAmount} health";
    }
}

public class Key : Item
{
    private readonly string _unlocksLocationId;

    public Key(string unlocksLocationId) : base("Mysterious Key", "A key that unlocks something", 1, 10)
    {
        _unlocksLocationId = unlocksLocationId;
    }

    public override void Use(Player player)
    {
        Console.WriteLine($"🔑 {player.Name} used the key, but nothing happened here.");
    }

    public bool CanUnlock(string locationId)
    {
        return locationId == _unlocksLocationId;
    }
}

public class Treasure : Item
{
    public Treasure() : base("Ancient Treasure", "A valuable artifact from a bygone era", 5, 1000)
    {
    }

    public override void Use(Player player)
    {
        Console.WriteLine($"💎 {player.Name} examined the treasure. It's worth {_value} gold!");
    }
}
