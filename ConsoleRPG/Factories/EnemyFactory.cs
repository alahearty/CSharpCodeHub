namespace ConsoleRPG.Factories;

using ConsoleRPG.Core;
using ConsoleRPG.Entities;
using ConsoleRPG.Skills;

// Factory class for creating enemies - demonstrates FACTORY PATTERN
public class EnemyFactory
{
    private readonly Random _random;

    public EnemyFactory()
    {
        _random = new Random();
    }

    public ICombatant CreateRandomEnemy(int playerLevel)
    {
        var enemyTypes = new[]
        {
            "Goblin",
            "Orc",
            "Skeleton",
            "Troll",
            "Dragon"
        };

        var enemyName = enemyTypes[_random.Next(enemyTypes.Length)];
        var level = Math.Max(1, playerLevel + _random.Next(-1, 2));
        
        return CreateEnemy(enemyName, level);
    }

    public ICombatant CreateEnemy(string name, int level)
    {
        var baseStats = GetBaseStats(name, level);
        
        var enemy = new Enemy(name, level)
        {
            Health = baseStats.Health,
            MaxHealth = baseStats.Health,
            Attack = baseStats.Attack,
            Defense = baseStats.Defense,
            ExperienceReward = baseStats.ExperienceReward
        };

        // Add skills based on enemy type
        if (level >= 3)
        {
            enemy.Skills.Add(new BasicAttack());
        }
        
        if (level >= 5)
        {
            enemy.Skills.Add(new HealSkill());
        }

        return enemy;
    }

    private (int Health, int Attack, int Defense, int ExperienceReward) GetBaseStats(string name, int level)
    {
        var baseHealth = name switch
        {
            "Goblin" => 40,
            "Orc" => 60,
            "Skeleton" => 50,
            "Troll" => 80,
            "Dragon" => 150,
            _ => 50
        };

        var baseAttack = name switch
        {
            "Goblin" => 10,
            "Orc" => 15,
            "Skeleton" => 12,
            "Troll" => 20,
            "Dragon" => 30,
            _ => 12
        };

        var baseDefense = name switch
        {
            "Goblin" => 5,
            "Orc" => 8,
            "Skeleton" => 6,
            "Troll" => 12,
            "Dragon" => 20,
            _ => 7
        };

        var experienceReward = name switch
        {
            "Goblin" => 20,
            "Orc" => 35,
            "Skeleton" => 25,
            "Troll" => 50,
            "Dragon" => 100,
            _ => 30
        };

        // Scale with level
        return (
            Health: baseHealth + (level - 1) * 10,
            Attack: baseAttack + (level - 1) * 3,
            Defense: baseDefense + (level - 1) * 2,
            ExperienceReward: experienceReward * level
        );
    }
}

