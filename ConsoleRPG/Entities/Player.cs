namespace ConsoleRPG.Entities;

using ConsoleRPG.Core;
using ConsoleRPG.Items;
using ConsoleRPG.Skills;

// Player class demonstrating INHERITANCE and ENCAPSULATION
public class Player : ICombatant
{
    public string Name { get; }
    public int Level { get; private set; }
    public int Health { get; private set; }
    public int MaxHealth { get; private set; }
    public int Attack { get; private set; }
    public int Defense { get; private set; }
    public bool IsAlive => Health > 0;
    public List<ISkill> Skills { get; }
    
    // Private fields with encapsulation
    private int _experience;
    private int _experienceToNextLevel;
    private readonly List<IItem> _inventory;
    private readonly List<IEquipment> _equippedItems;

    public int Experience => _experience;
    public int ExperienceToNextLevel => _experienceToNextLevel;
    public IReadOnlyList<IItem> Inventory => _inventory.AsReadOnly();
    public IReadOnlyList<IEquipment> EquippedItems => _equippedItems.AsReadOnly();

    public Player(string name)
    {
        Name = name;
        Level = 1;
        Health = 100;
        MaxHealth = 100;
        Attack = 15;
        Defense = 10;
        _experience = 0;
        _experienceToNextLevel = 100;
        Skills = new List<ISkill>
        {
            new BasicAttack(),
            new HealSkill()
        };
        _inventory = new List<IItem>();
        _equippedItems = new List<IEquipment>();
    }

    public void TakeDamage(int damage)
    {
        var actualDamage = Math.Max(1, damage - Defense);
        Health = Math.Max(0, Health - actualDamage);
        
        Console.WriteLine($"💥 {Name} took {actualDamage} damage! Health: {Health}/{MaxHealth}");
        
        if (!IsAlive)
        {
            Console.WriteLine($"💀 {Name} has fallen in battle!");
        }
    }

    public void Heal(int amount)
    {
        if (IsAlive)
        {
            var oldHealth = Health;
            Health = Math.Min(MaxHealth, Health + amount);
            var healedAmount = Health - oldHealth;
            
            if (healedAmount > 0)
            {
                Console.WriteLine($"💚 {Name} healed {healedAmount} health. Health: {Health}/{MaxHealth}");
            }
        }
    }

    public new void Attack(ICombatant target)
    {
        if (!IsAlive || target == null || !target.IsAlive)
            return;

        var damage = CalculateDamage();
        target.TakeDamage(damage);
        
        Console.WriteLine($"⚔️ {Name} attacks {target.Name} for {damage} damage!");
    }

    public void UseSkill(ISkill skill, ICombatant target)
    {
        if (!IsAlive || skill == null)
            return;

        if (Skills.Contains(skill))
        {
            skill.Use(this, target);
        }
        else
        {
            Console.WriteLine($"❌ {Name} doesn't know that skill!");
        }
    }

    public void GainExperience(int amount)
    {
        if (!IsAlive) return;

        _experience += amount;
        Console.WriteLine($"✨ {Name} gained {amount} experience! ({_experience}/{_experienceToNextLevel})");
        
        if (_experience >= _experienceToNextLevel)
        {
            LevelUp();
        }
    }

    public void LevelUp()
    {
        Level++;
        _experience -= _experienceToNextLevel;
        _experienceToNextLevel = Level * 100;
        
        // Increase stats
        MaxHealth += 20;
        Health = MaxHealth; // Full heal on level up
        Attack += 5;
        Defense += 3;
        
        Console.WriteLine($"🎉 {Name} reached level {Level}!");
        Console.WriteLine($"📊 Stats increased! Health: {MaxHealth}, Attack: {Attack}, Defense: {Defense}");
    }

    public void AddToInventory(IItem item)
    {
        if (item != null)
        {
            _inventory.Add(item);
            Console.WriteLine($"📦 {Name} obtained {item.Name}!");
        }
    }

    public void EquipItem(IEquipment equipment)
    {
        if (equipment != null && _inventory.Contains(equipment))
        {
            // Remove old equipment of same type if exists
            var oldEquipment = _equippedItems.FirstOrDefault(e => e.EquipmentType == equipment.EquipmentType);
            if (oldEquipment != null)
            {
                _equippedItems.Remove(oldEquipment);
                UnequipStats(oldEquipment);
            }
            
            _equippedItems.Add(equipment);
            EquipStats(equipment);
            Console.WriteLine($"⚔️ {Name} equipped {equipment.Name}!");
        }
    }

    private void EquipStats(IEquipment equipment)
    {
        Attack += equipment.AttackBonus;
        Defense += equipment.DefenseBonus;
        MaxHealth += equipment.HealthBonus;
        Health += equipment.HealthBonus; // Increase current health too
    }

    private void UnequipStats(IEquipment equipment)
    {
        Attack -= equipment.AttackBonus;
        Defense -= equipment.DefenseBonus;
        MaxHealth -= equipment.HealthBonus;
        Health = Math.Min(Health, MaxHealth); // Ensure health doesn't exceed max
    }

    private int CalculateDamage()
    {
        var baseDamage = Attack;
        var random = new Random();
        var variation = random.Next(-3, 4); // -3 to +3
        return Math.Max(1, baseDamage + variation);
    }

    public void ShowStatus()
    {
        Console.WriteLine($"\n👤 {Name} - Level {Level}");
        Console.WriteLine($"❤️  Health: {Health}/{MaxHealth}");
        Console.WriteLine($"⚔️  Attack: {Attack}");
        Console.WriteLine($"🛡️  Defense: {Defense}");
        Console.WriteLine($"✨ Experience: {_experience}/{_experienceToNextLevel}");
        
        if (_equippedItems.Count > 0)
        {
            Console.WriteLine("\n⚔️  Equipped Items:");
            foreach (var item in _equippedItems)
            {
                Console.WriteLine($"  - {item.Name}");
            }
        }
    }
}
