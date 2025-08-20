namespace ConsoleRPG;

using ConsoleRPG.Core;
using ConsoleRPG.Entities;
using ConsoleRPG.Combat;
using ConsoleRPG.Items;
using ConsoleRPG.Factories;

// Main game class demonstrating DEPENDENCY INVERSION PRINCIPLE
public class RPGGame
{
    private readonly Player _player;
    private readonly CombatSystem _combatSystem;
    private readonly EnemyFactory _enemyFactory;
    private readonly ItemFactory _itemFactory;
    private bool _isRunning;

    public RPGGame()
    {
        _player = new Player("Hero");
        _combatSystem = new CombatSystem();
        _enemyFactory = new EnemyFactory();
        _itemFactory = new ItemFactory();
        _isRunning = false;
    }

    public void Start()
    {
        Console.WriteLine("🎮 Welcome to the Console RPG Game!");
        Console.WriteLine("You are a brave hero on a quest to defeat monsters and gain power.\n");
        
        _isRunning = true;
        
        // Give player some starting items
        var startingSword = _itemFactory.CreateWeapon("Iron Sword", 10, 5, 0);
        var startingArmor = _itemFactory.CreateArmor("Leather Armor", 0, 8, 20);
        
        _player.AddToInventory(startingSword);
        _player.AddToInventory(startingArmor);
        
        _player.EquipItem(startingSword);
        _player.EquipItem(startingArmor);
        
        Console.WriteLine("📦 You received starting equipment!");
        
        // Main game loop
        while (_isRunning && _player.IsAlive)
        {
            ShowMainMenu();
            var choice = Console.ReadLine()?.Trim();
            ProcessMainMenuChoice(choice);
        }
        
        if (!_player.IsAlive)
        {
            Console.WriteLine("\n💀 Game Over! You have fallen in battle.");
        }
        else
        {
            Console.WriteLine("\n👋 Thanks for playing!");
        }
    }

    private void ShowMainMenu()
    {
        Console.WriteLine("\n" + new string('=', 40));
        Console.WriteLine("🏰 MAIN MENU");
        Console.WriteLine("=" + new string('=', 39));
        Console.WriteLine("1. 🗺️  Explore (Find enemies)");
        Console.WriteLine("2. ⚔️  Show Status");
        Console.WriteLine("3. 📦 Show Inventory");
        Console.WriteLine("4. 🎯 Show Skills");
        Console.WriteLine("5. 🚪 Exit Game");
        Console.WriteLine(new string('=', 40));
        Console.Write("Choose option (1-5): ");
    }

    private void ProcessMainMenuChoice(string? choice)
    {
        switch (choice)
        {
            case "1":
                Explore();
                break;
            case "2":
                _player.ShowStatus();
                break;
            case "3":
                ShowInventory();
                break;
            case "4":
                ShowSkills();
                break;
            case "5":
                _isRunning = false;
                Console.WriteLine("👋 Exiting game...");
                break;
            default:
                Console.WriteLine("❌ Invalid choice. Please select 1-5.");
                break;
        }
    }

    private void Explore()
    {
        Console.WriteLine("\n🗺️  Exploring the world...");
        Thread.Sleep(1000);
        
        var random = new Random();
        var encounterChance = random.Next(1, 101);
        
        if (encounterChance <= 70) // 70% chance of encounter
        {
            var enemy = _enemyFactory.CreateRandomEnemy(_player.Level);
            Console.WriteLine($"\n👹 You encountered a {enemy.Name}!");
            
            Console.Write("Do you want to fight? (y/n): ");
            var choice = Console.ReadLine()?.Trim().ToLower();
            
            if (choice == "y" || choice == "yes")
            {
                _combatSystem.StartCombat(_player, enemy);
                
                // Chance to find items after victory
                if (_player.IsAlive && random.Next(1, 101) <= 40)
                {
                    var item = _itemFactory.CreateRandomItem();
                    _player.AddToInventory(item);
                }
            }
            else
            {
                Console.WriteLine("🏃 You ran away from the battle.");
            }
        }
        else
        {
            Console.WriteLine("🌿 The area is peaceful. No enemies found.");
            
            // Chance to find items while exploring
            if (random.Next(1, 101) <= 20)
            {
                var item = _itemFactory.CreateRandomItem();
                _player.AddToInventory(item);
            }
        }
    }

    private void ShowInventory()
    {
        Console.WriteLine($"\n📦 {_player.Name}'s Inventory:");
        var inventory = _player.Inventory;
        
        if (inventory.Count == 0)
        {
            Console.WriteLine("  Empty");
        }
        else
        {
            for (int i = 0; i < inventory.Count; i++)
            {
                var item = inventory[i];
                Console.WriteLine($"  {i + 1}. {item.Name}: {item.Description}");
                
                if (item is IEquipment equipment)
                {
                    Console.WriteLine($"     Stats: +{equipment.AttackBonus} ATK, +{equipment.DefenseBonus} DEF, +{equipment.HealthBonus} HP");
                }
            }
            
            Console.Write("\nDo you want to equip an item? (y/n): ");
            var choice = Console.ReadLine()?.Trim().ToLower();
            
            if (choice == "y" || choice == "yes")
            {
                Console.Write("Enter item number to equip: ");
                if (int.TryParse(Console.ReadLine(), out int itemNumber) && 
                    itemNumber > 0 && itemNumber <= inventory.Count)
                {
                    var selectedItem = inventory[itemNumber - 1];
                    if (selectedItem is IEquipment equipment)
                    {
                        _player.EquipItem(equipment);
                    }
                    else
                    {
                        Console.WriteLine("❌ That item cannot be equipped.");
                    }
                }
                else
                {
                    Console.WriteLine("❌ Invalid item number.");
                }
            }
        }
    }

    private void ShowSkills()
    {
        Console.WriteLine($"\n🎯 {_player.Name}'s Skills:");
        var skills = _player.Skills;
        
        if (skills.Count == 0)
        {
            Console.WriteLine("  No skills learned yet.");
        }
        else
        {
            for (int i = 0; i < skills.Count; i++)
            {
                var skill = skills[i];
                Console.WriteLine($"  {i + 1}. {skill.Name}");
                Console.WriteLine($"     {skill.Description}");
                Console.WriteLine($"     Cost: {skill.Cost} MP");
            }
        }
    }
}
