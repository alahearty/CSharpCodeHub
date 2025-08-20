using BankingSystem.Models;
using BankingSystem.Services;

namespace BankingSystem;

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("🏦 Advanced OOP & SOLID Principles - Banking System Tutorial");
        Console.WriteLine("===========================================================\n");

        // Demonstrate Inheritance and Polymorphism
        DemonstrateInheritanceAndPolymorphism();
        
        Console.WriteLine("\n" + new string('-', 60) + "\n");
        
        // Demonstrate Single Responsibility Principle
        DemonstrateSingleResponsibilityPrinciple();
        
        Console.WriteLine("\n" + new string('-', 60) + "\n");
        
        // Demonstrate Encapsulation
        DemonstrateEncapsulation();
        
        Console.WriteLine("\n" + new string('-', 60) + "\n");
        
        // Demonstrate Abstraction
        DemonstrateAbstraction();
        
        Console.WriteLine("\nPress any key to exit...");
        Console.ReadKey();
    }

    static void DemonstrateInheritanceAndPolymorphism()
    {
        Console.WriteLine("🔹 INHERITANCE & POLYMORPHISM DEMONSTRATION");
        Console.WriteLine("===========================================");
        
        // Create different types of accounts
        var accounts = new List<BankAccount>
        {
            new SavingsAccount("SA001", "John Doe", 1000.0m, 0.05m),
            new CheckingAccount("CA001", "Jane Smith", 2500.0m, 1000.0m),
            new BusinessAccount("BA001", "Tech Corp", 10000.0m, 5000.0m)
        };

        // Demonstrate polymorphic behavior
        foreach (var account in accounts)
        {
            Console.WriteLine($"\nAccount: {account.AccountNumber}");
            Console.WriteLine($"Type: {account.GetType().Name}");
            Console.WriteLine($"Balance: ${account.Balance:F2}");
            
            // Polymorphic method calls
            account.Deposit(500.0m);
            account.Withdraw(200.0m);
            
            // Each account type has different interest calculation
            if (account is SavingsAccount savings)
            {
                var interest = savings.CalculateInterest();
                Console.WriteLine($"Interest earned: ${interest:F2}");
            }
            
            Console.WriteLine($"Final Balance: ${account.Balance:F2}");
        }
    }

    static void DemonstrateSingleResponsibilityPrinciple()
    {
        Console.WriteLine("🔹 SINGLE RESPONSIBILITY PRINCIPLE DEMONSTRATION");
        Console.WriteLine("==============================================");
        
        // Each service has a single responsibility
        var accountService = new AccountService();
        var transactionService = new TransactionService();
        var notificationService = new NotificationService();
        
        var account = new SavingsAccount("SA002", "Bob Wilson", 2000.0m, 0.04m);
        
        Console.WriteLine($"\nInitial balance: ${account.Balance:F2}");
        
        // Account service handles account operations
        accountService.UpdateAccountInfo(account, "Bob Wilson Jr.");
        
        // Transaction service handles transactions
        var transaction = transactionService.ProcessTransaction(account, 300.0m, TransactionType.Deposit);
        
        // Notification service handles notifications
        notificationService.SendTransactionNotification(transaction);
        
        Console.WriteLine($"Final balance: ${account.Balance:F2}");
    }

    static void DemonstrateEncapsulation()
    {
        Console.WriteLine("🔹 ENCAPSULATION DEMONSTRATION");
        Console.WriteLine("=============================");
        
        var account = new SavingsAccount("SA003", "Alice Johnson", 1500.0m, 0.06m);
        
        Console.WriteLine($"\nAccount holder: {account.AccountHolderName}");
        Console.WriteLine($"Balance: ${account.Balance:F2}");
        
        // Try to access private fields (this would cause compilation error)
        // account._balance = -1000; // This is not allowed!
        
        // Only public methods can modify the state
        account.Deposit(100.0m);
        Console.WriteLine($"After deposit: ${account.Balance:F2}");
        
        // Validation prevents invalid operations
        var result = account.Withdraw(2000.0m); // More than balance
        Console.WriteLine($"Withdrawal result: {result}");
        Console.WriteLine($"Balance remains: ${account.Balance:F2}");
    }

    static void DemonstrateAbstraction()
    {
        Console.WriteLine("🔹 ABSTRACTION DEMONSTRATION");
        Console.WriteLine("============================");
        
        // Using abstract base class
        BankAccount account = new SavingsAccount("SA004", "Charlie Brown", 3000.0m, 0.07m);
        
        Console.WriteLine($"\nAccount type: {account.GetType().Name}");
        
        // Abstract methods provide a contract
        account.Deposit(500.0m);
        account.Withdraw(100.0m);
        
        // Abstract properties
        Console.WriteLine($"Account number: {account.AccountNumber}");
        Console.WriteLine($"Account holder: {account.AccountHolderName}");
        Console.WriteLine($"Current balance: ${account.Balance:F2}");
        
        // Using interface for additional behavior
        if (account is IInterestBearing interestAccount)
        {
            var interest = interestAccount.CalculateInterest();
            Console.WriteLine($"Interest earned: ${interest:F2}");
        }
    }
}
