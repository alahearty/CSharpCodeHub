using ConsoleRPG.Core;
using ConsoleRPG.Entities;
using ConsoleRPG.Combat;
using ConsoleRPG.Items;
using ConsoleRPG.Skills;
using ConsoleRPG.Factories;

namespace ConsoleRPG;

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("⚔️  Advanced OOP & SOLID Principles - Console RPG Game");
        Console.WriteLine("======================================================\n");

        // Initialize game
        var game = new RPGGame();
        
        // Start the game
        game.Start();
        
        Console.WriteLine("\nPress any key to exit...");
        Console.ReadKey();
    }
}
