using TextAdventureGame.Core;
using TextAdventureGame.Engine;
using TextAdventureGame.Entities;
using TextAdventureGame.Commands;
using TextAdventureGame.Factories;

namespace TextAdventureGame;

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("🗺️  Advanced OOP & SOLID Principles - Text Adventure Game");
        Console.WriteLine("==========================================================\n");

        // Initialize game engine
        var gameEngine = new GameEngine();
        
        // Start the game
        gameEngine.StartGame();
        
        Console.WriteLine("\nPress any key to exit...");
        Console.ReadKey();
    }
}
