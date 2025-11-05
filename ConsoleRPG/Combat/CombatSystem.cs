namespace ConsoleRPG.Combat;

using ConsoleRPG.Core;
using ConsoleRPG.Entities;

// Combat system demonstrating STRATEGY PATTERN
public class CombatSystem
{
    public void StartCombat(ICombatant player, ICombatant enemy)
    {
        Console.WriteLine($"\n⚔️  Combat started! {player.Name} vs {enemy.Name}");
        Console.WriteLine("================================================");
        
        var round = 1;
        
        while (player.IsAlive && enemy.IsAlive)
        {
            Console.WriteLine($"\n🔄 Round {round}");
            Console.WriteLine($"{player.Name}: {player.Health}/{player.MaxHealth} HP");
            Console.WriteLine($"{enemy.Name}: {enemy.Health}/{enemy.MaxHealth} HP");
            
            // Player's turn
            if (player.IsAlive)
            {
                PlayerTurn(player, enemy);
            }
            
            // Enemy's turn
            if (enemy.IsAlive)
            {
                EnemyTurn(enemy, player);
            }
            
            round++;
            
            // Add some delay for readability
            Thread.Sleep(1000);
        }
        
        // Combat result
        if (player.IsAlive)
        {
            Console.WriteLine($"\n🎉 Victory! {player.Name} defeated {enemy.Name}!");
            if (enemy is Enemy enemyEntity)
            {
                player.GainExperience(enemyEntity.ExperienceReward);
            }
        }
        else
        {
            Console.WriteLine($"\n💀 Defeat! {enemy.Name} defeated {player.Name}!");
        }
    }

    private void PlayerTurn(ICombatant player, ICombatant enemy)
    {
        Console.WriteLine($"\n👤 {player.Name}'s turn:");
        Console.WriteLine("1. Attack");
        Console.WriteLine("2. Use Skill");
        Console.WriteLine("3. Show Status");
        
        Console.Write("Choose action (1-3): ");
        var choice = Console.ReadLine()?.Trim();
        
        switch (choice)
        {
            case "1":
                player.Attack(enemy);
                break;
            case "2":
                UseSkill(player, enemy);
                break;
            case "3":
                if (player is Entities.Player playerEntity)
                {
                    playerEntity.ShowStatus();
                }
                break;
            default:
                Console.WriteLine("Invalid choice. Using basic attack.");
                player.Attack(enemy);
                break;
        }
    }

    private void UseSkill(ICombatant player, ICombatant enemy)
    {
        if (player.Skills.Count == 0)
        {
            Console.WriteLine("No skills available. Using basic attack.");
            player.Attack(enemy);
            return;
        }
        
        Console.WriteLine("\n🎯 Available Skills:");
        for (int i = 0; i < player.Skills.Count; i++)
        {
            var skill = player.Skills[i];
            Console.WriteLine($"{i + 1}. {skill.Name} - {skill.Description}");
        }
        
        Console.Write("Choose skill (1-{0}): ", player.Skills.Count);
        if (int.TryParse(Console.ReadLine(), out int skillChoice) && 
            skillChoice > 0 && skillChoice <= player.Skills.Count)
        {
            var selectedSkill = player.Skills[skillChoice - 1];
            player.UseSkill(selectedSkill, enemy);
        }
        else
        {
            Console.WriteLine("Invalid choice. Using basic attack.");
            player.Attack(enemy);
        }
    }

    private void EnemyTurn(ICombatant enemy, ICombatant player)
    {
        Console.WriteLine($"\n👹 {enemy.Name}'s turn:");
        
        // Simple AI: randomly choose between attack and skill
        var random = new Random();
        var choice = random.Next(1, 4);
        
        switch (choice)
        {
            case 1:
                enemy.Attack(player);
                break;
            case 2:
                if (enemy.Skills.Count > 0)
                {
                    var randomSkill = enemy.Skills[random.Next(enemy.Skills.Count)];
                    enemy.UseSkill(randomSkill, player);
                }
                else
                {
                    enemy.Attack(player);
                }
                break;
            case 3:
                // Enemy heals if health is low
                if (enemy.Health < enemy.MaxHealth / 2)
                {
                    enemy.Heal(enemy.MaxHealth / 4);
                    Console.WriteLine($"💚 {enemy.Name} healed themselves!");
                }
                else
                {
                    enemy.Attack(player);
                }
                break;
        }
    }
}
