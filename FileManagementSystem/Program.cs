using FileManagementSystem.Core;
using FileManagementSystem.Services;
using FileManagementSystem.Models;

namespace FileManagementSystem;

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("📁 Advanced File Management System");
        Console.WriteLine("==================================\n");

        var fileManager = new FileManager();
        var fileAnalyzer = new FileAnalyzer();
        var fileOrganizer = new FileOrganizer();
        
        bool isRunning = true;
        
        while (isRunning)
        {
            ShowMainMenu();
            var choice = Console.ReadLine()?.Trim();
            
            switch (choice)
            {
                case "1":
                    fileManager.ShowCurrentDirectory();
                    break;
                case "2":
                    fileManager.ListFiles();
                    break;
                case "3":
                    fileManager.SearchFiles();
                    break;
                case "4":
                    fileManager.CreateFile();
                    break;
                case "5":
                    fileManager.DeleteFile();
                    break;
                case "6":
                    fileAnalyzer.AnalyzeDirectory();
                    break;
                case "7":
                    fileOrganizer.OrganizeByType();
                    break;
                case "8":
                    fileOrganizer.OrganizeByDate();
                    break;
                case "9":
                    fileManager.ShowFileInfo();
                    break;
                case "0":
                    isRunning = false;
                    Console.WriteLine("👋 Goodbye!");
                    break;
                default:
                    Console.WriteLine("❌ Invalid choice. Please try again.");
                    break;
            }
            
            if (isRunning)
            {
                Console.WriteLine("\nPress any key to continue...");
                Console.ReadKey();
                Console.Clear();
            }
        }
    }

    static void ShowMainMenu()
    {
        Console.WriteLine("📁 FILE MANAGEMENT SYSTEM");
        Console.WriteLine("=========================");
        Console.WriteLine("1. 📂 Show Current Directory");
        Console.WriteLine("2. 📋 List Files");
        Console.WriteLine("3. 🔍 Search Files");
        Console.WriteLine("4. ➕ Create File");
        Console.WriteLine("5. 🗑️  Delete File");
        Console.WriteLine("6. 📊 Analyze Directory");
        Console.WriteLine("7. 🗂️  Organize by Type");
        Console.WriteLine("8. 📅 Organize by Date");
        Console.WriteLine("9. ℹ️  Show File Info");
        Console.WriteLine("0. 🚪 Exit");
        Console.WriteLine("=========================");
        Console.Write("Choose option (0-9): ");
    }
}
