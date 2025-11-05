namespace ConsoleRPG.Skills;

using ConsoleRPG.Core;

// Basic attack skill - demonstrates POLYMORPHISM
public class BasicAttack : ISkill
{
    public string Name => "Basic Attack";
    public string Description => "A basic physical attack";
    public int Cost => 0;

    public void Use(ICombatant user, ICombatant target)
    {
        if (user == null || target == null || !user.IsAlive || !target.IsAlive)
            return;

        var damage = CalculateDamage(user);
        target.TakeDamage(damage);
        
        Console.WriteLine($"⚔️ {user.Name} uses {Name} on {target.Name} for {damage} damage!");
    }

    private int CalculateDamage(ICombatant user)
    {
        var baseDamage = user.Attack;
        var random = new Random();
        var variation = random.Next(-2, 3); // -2 to +2
        return Math.Max(1, baseDamage + variation);
    }
}

