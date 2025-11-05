namespace ConsoleRPG.Entities;

using ConsoleRPG.Core;
using ConsoleRPG.Skills;

// Enemy class demonstrating INHERITANCE and ENCAPSULATION
public class Enemy : ICombatant
{
    public string Name { get; }
    public int Level { get; }
    public int Health { get; set; }
    public int MaxHealth { get; set; }
    public int Attack { get; set; }
    public int Defense { get; set; }
    public bool IsAlive => Health > 0;
    public List<ISkill> Skills { get; }
    
    public int ExperienceReward { get; set; }

    public Enemy(string name, int level)
    {
        Name = name;
        Level = level;
        Skills = new List<ISkill>();
    }

    public void TakeDamage(int damage)
    {
        var actualDamage = Math.Max(1, damage - Defense);
        Health = Math.Max(0, Health - actualDamage);
        
        Console.WriteLine($"💥 {Name} took {actualDamage} damage! Health: {Health}/{MaxHealth}");
        
        if (!IsAlive)
        {
            Console.WriteLine($"💀 {Name} has been defeated!");
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

    public void Attack(ICombatant target)
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
    }

    public void GainExperience(int amount)
    {
        // Enemies don't gain experience
    }

    public void LevelUp()
    {
        // Enemies don't level up
    }

    private int CalculateDamage()
    {
        var baseDamage = Attack;
        var random = new Random();
        var variation = random.Next(-3, 4); // -3 to +3
        return Math.Max(1, baseDamage + variation);
    }
}

