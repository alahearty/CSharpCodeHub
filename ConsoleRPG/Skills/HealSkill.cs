namespace ConsoleRPG.Skills;

using ConsoleRPG.Core;

// Heal skill - demonstrates POLYMORPHISM
public class HealSkill : ISkill
{
    public string Name => "Heal";
    public string Description => "Restores health points";
    public int Cost => 10;

    public void Use(ICombatant user, ICombatant target)
    {
        if (user == null || target == null || !user.IsAlive || !target.IsAlive)
            return;

        var healAmount = CalculateHeal(user);
        target.Heal(healAmount);
        
        Console.WriteLine($"💚 {user.Name} uses {Name} on {target.Name} for {healAmount} health!");
    }

    private int CalculateHeal(ICombatant user)
    {
        var baseHeal = 20 + (user.Level * 5);
        var random = new Random();
        var variation = random.Next(-5, 6); // -5 to +5
        return Math.Max(10, baseHeal + variation);
    }
}

