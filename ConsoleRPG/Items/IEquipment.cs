namespace ConsoleRPG.Items;

// Interface for equippable items - demonstrates INTERFACE SEGREGATION
public interface IEquipment : IItem
{
    EquipmentType EquipmentType { get; }
    int AttackBonus { get; }
    int DefenseBonus { get; }
    int HealthBonus { get; }
}

public enum EquipmentType
{
    Weapon,
    Armor,
    Accessory
}

