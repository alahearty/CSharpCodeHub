using InventoryManagementSystem.Core;
using InventoryManagementSystem.Services;
using InventoryManagementSystem.Models;

namespace InventoryManagementSystem;

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("📦 Inventory Management System");
        Console.WriteLine("==============================\n");

        var inventoryService = new InventoryService();
        var supplierService = new SupplierService();
        var transactionService = new TransactionService();
        var reportService = new ReportService();
        
        bool isRunning = true;
        
        while (isRunning)
        {
            ShowMainMenu();
            var choice = Console.ReadLine()?.Trim();
            
            switch (choice)
            {
                case "1":
                    inventoryService.AddProduct();
                    break;
                case "2":
                    inventoryService.UpdateProduct();
                    break;
                case "3":
                    inventoryService.RemoveProduct();
                    break;
                case "4":
                    inventoryService.ViewProduct();
                    break;
                case "5":
                    inventoryService.ListAllProducts();
                    break;
                case "6":
                    inventoryService.SearchProducts();
                    break;
                case "7":
                    supplierService.ManageSuppliers();
                    break;
                case "8":
                    transactionService.ProcessTransaction();
                    break;
                case "9":
                    reportService.GenerateInventoryReport();
                    break;
                case "10":
                    reportService.GenerateLowStockReport();
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
        Console.WriteLine("📦 INVENTORY MANAGEMENT SYSTEM");
        Console.WriteLine("==============================");
        Console.WriteLine("1. ➕ Add Product");
        Console.WriteLine("2. ✏️  Update Product");
        Console.WriteLine("3. 🗑️  Remove Product");
        Console.WriteLine("4. 👁️  View Product");
        Console.WriteLine("5. 📋 List All Products");
        Console.WriteLine("6. 🔍 Search Products");
        Console.WriteLine("7. 🏢 Manage Suppliers");
        Console.WriteLine("8. 💰 Process Transaction");
        Console.WriteLine("9. 📊 Inventory Report");
        Console.WriteLine("10. ⚠️  Low Stock Report");
        Console.WriteLine("0. 🚪 Exit");
        Console.WriteLine("==============================");
        Console.Write("Choose option (0-10): ");
    }
}
