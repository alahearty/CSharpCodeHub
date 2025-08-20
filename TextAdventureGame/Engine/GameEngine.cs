namespace TextAdventureGame.Engine;

using TextAdventureGame.Core;
using TextAdventureGame.Entities;
using TextAdventureGame.Commands;
using TextAdventureGame.Factories;

// Game engine demonstrating DEPENDENCY INVERSION PRINCIPLE
public class GameEngine
{
    private readonly Player _player;
    private readonly List<ICommand> _commands;
    private readonly GameWorld _world;
    private bool _isRunning;

    public GameEngine()
    {
        _player = new Player("Adventurer");
        _commands = new List<ICommand>
        {
            new MoveCommand(),
            new TakeCommand(),
            new UseCommand(),
            new InventoryCommand(),
            new LookCommand(),
            new HelpCommand()
        };
        _world = new GameWorld();
        _isRunning = false;
    }

    public void StartGame()
    {
        Console.WriteLine("🎮 Welcome to the Text Adventure Game!");
        Console.WriteLine("Type 'help' for available commands.\n");
        
        // Place player in starting location
        _player.MoveTo(_world.StartingLocation);
        ShowLocationDescription(_player);
        
        _isRunning = true;
        
        // Main game loop
        while (_isRunning && _player.IsActive)
        {
            Console.Write("\n> ");
            var input = Console.ReadLine()?.Trim();
            
            if (string.IsNullOrEmpty(input))
                continue;
                
            ProcessCommand(input);
        }
        
        if (!_player.IsActive)
        {
            Console.WriteLine("\n💀 Game Over! You have fallen in battle.");
        }
        else
        {
            Console.WriteLine("\n👋 Thanks for playing!");
        }
    }

    private void ProcessCommand(string input)
    {
        var parts = input.Split(' ', StringSplitOptions.RemoveEmptyEntries);
        if (parts.Length == 0) return;

        var commandName = parts[0].ToLower();
        var parameters = parts.Skip(1).ToArray();

        var command = _commands.FirstOrDefault(cmd => cmd.Name == commandName);
        
        if (command != null)
        {
            if (command.CanExecute(_player))
            {
                command.Execute(_player, parameters);
            }
            else
            {
                Console.WriteLine("❌ Cannot execute that command right now.");
            }
        }
        else
        {
            Console.WriteLine($"❌ Unknown command: {commandName}");
            Console.WriteLine("Type 'help' for available commands.");
        }
    }

    private void ShowLocationDescription(Player player)
    {
        var location = player.CurrentLocation;
        Console.WriteLine($"\n🏞️  {location.Name}");
        Console.WriteLine($"   {location.Description}");
        
        if (location.Objects.Count > 0)
        {
            Console.WriteLine("\n📦 Objects here:");
            foreach (var obj in location.Objects.Where(o => o.IsActive))
            {
                Console.WriteLine($"  - {obj.Name}: {obj.Description}");
            }
        }
    }
}
