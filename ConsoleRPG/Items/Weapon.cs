namespace ConsoleRPG.Items;

// Weapon class demonstrating INHERITANCE
public class Weapon : IEquipment
{
    public string Name { get; }
    public string Description { get; }
    public EquipmentType EquipmentType => EquipmentType.Weapon;
    public int AttackBonus { get; }
    public int DefenseBonus { get; }
    public int HealthBonus { get; }

    public Weapon(string name, int attackBonus, int defenseBonus, int healthBonus, string description = "")
    {
        Name = name;
        AttackBonus = attackBonus;
        DefenseBonus = defenseBonus;
        HealthBonus = healthBonus;
        Description = string.IsNullOrEmpty(description) 
            ? $"A weapon that increases attack by {attackBonus}" 
            : description;
    }
}

