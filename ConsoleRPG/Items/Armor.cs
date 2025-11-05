namespace ConsoleRPG.Items;

// Armor class demonstrating INHERITANCE
public class Armor : IEquipment
{
    public string Name { get; }
    public string Description { get; }
    public EquipmentType EquipmentType => EquipmentType.Armor;
    public int AttackBonus { get; }
    public int DefenseBonus { get; }
    public int HealthBonus { get; }

    public Armor(string name, int attackBonus, int defenseBonus, int healthBonus, string description = "")
    {
        Name = name;
        AttackBonus = attackBonus;
        DefenseBonus = defenseBonus;
        HealthBonus = healthBonus;
        Description = string.IsNullOrEmpty(description) 
            ? $"Armor that increases defense by {defenseBonus} and health by {healthBonus}" 
            : description;
    }
}

