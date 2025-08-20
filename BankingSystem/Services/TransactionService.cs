namespace BankingSystem.Services;

using BankingSystem.Models;

// Service demonstrating SINGLE RESPONSIBILITY PRINCIPLE
// This service only handles transaction-related operations
public class TransactionService
{
    public Transaction ProcessTransaction(BankAccount account, decimal amount, TransactionType type)
    {
        if (account == null)
            throw new ArgumentNullException(nameof(account));

        var transaction = new Transaction(account.AccountNumber, amount, type);
        
        bool success = false;
        
        switch (type)
        {
            case TransactionType.Deposit:
                success = account.Deposit(amount);
                break;
            case TransactionType.Withdrawal:
                success = account.Withdraw(amount);
                break;
            case TransactionType.Transfer:
                // For transfer, we would need a target account
                // This is simplified for demonstration
                success = account.Withdraw(amount);
                break;
        }

        transaction.IsSuccessful = success;
        
        if (success)
        {
            Console.WriteLine($"Transaction {transaction.TransactionId}: {type} of ${amount:F2} processed successfully");
        }
        else
        {
            Console.WriteLine($"Transaction {transaction.TransactionId}: {type} of ${amount:F2} failed");
        }

        return transaction;
    }

    public List<Transaction> GetTransactionHistory(string accountNumber)
    {
        // In a real application, this would query a database
        // For demonstration, we return an empty list
        return new List<Transaction>();
    }

    public decimal CalculateTransactionFee(Transaction transaction)
    {
        if (transaction == null)
            return 0;

        // Simple fee calculation based on transaction type
        return transaction.Type switch
        {
            TransactionType.Deposit => 0, // No fee for deposits
            TransactionType.Withdrawal => 2.50m, // $2.50 fee for withdrawals
            TransactionType.Transfer => 5.00m, // $5.00 fee for transfers
            _ => 0
        };
    }
}
