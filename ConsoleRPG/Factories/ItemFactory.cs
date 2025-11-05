namespace ConsoleRPG.Factories;

using ConsoleRPG.Items;

// Factory class for creating items - demonstrates FACTORY PATTERN
public class ItemFactory
{
    private readonly Random _random;

    public ItemFactory()
    {
        _random = new Random();
    }

    public IEquipment CreateWeapon(string name, int attackBonus, int defenseBonus, int healthBonus, string description = "")
    {
        return new Weapon(name, attackBonus, defenseBonus, healthBonus, description);
    }

    public IEquipment CreateArmor(string name, int attackBonus, int defenseBonus, int healthBonus, string description = "")
    {
        return new Armor(name, attackBonus, defenseBonus, healthBonus, description);
    }

    public IItem CreateRandomItem()
    {
        var itemType = _random.Next(0, 2);
        
        return itemType switch
        {
            0 => CreateRandomWeapon(),
            1 => CreateRandomArmor(),
            _ => CreateRandomWeapon()
        };
    }

    private IEquipment CreateRandomWeapon()
    {
        var weapons = new[]
        {
            ("Iron Sword", 8, 2, 0),
            ("Steel Sword", 12, 3, 5),
            ("Magic Sword", 15, 5, 10),
            ("Dragon Blade", 20, 8, 15)
        };

        var weapon = weapons[_random.Next(weapons.Length)];
        return CreateWeapon(weapon.Item1, weapon.Item2, weapon.Item3, weapon.Item4);
    }

    private IEquipment CreateRandomArmor()
    {
        var armors = new[]
        {
            ("Leather Armor", 0, 8, 20),
            ("Chain Mail", 2, 12, 30),
            ("Plate Armor", 3, 18, 40),
            ("Dragon Scale", 5, 25, 50)
        };

        var armor = armors[_random.Next(armors.Length)];
        return CreateArmor(armor.Item1, armor.Item2, armor.Item3, armor.Item4);
    }
}

