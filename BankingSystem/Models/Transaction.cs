namespace BankingSystem.Models;

public enum TransactionType
{
    Deposit,
    Withdrawal,
    Transfer
}

// Model class for transactions
public class Transaction
{
    public string TransactionId { get; }
    public string AccountNumber { get; }
    public decimal Amount { get; }
    public TransactionType Type { get; }
    public DateTime Timestamp { get; }
    public bool IsSuccessful { get; set; }

    public Transaction(string accountNumber, decimal amount, TransactionType type)
    {
        TransactionId = Guid.NewGuid().ToString("N")[..8].ToUpper();
        AccountNumber = accountNumber ?? throw new ArgumentNullException(nameof(accountNumber));
        Amount = amount;
        Type = type;
        Timestamp = DateTime.Now;
        IsSuccessful = false;
    }

    public override string ToString()
    {
        return $"{Type} of ${Amount:F2} on {Timestamp:yyyy-MM-dd HH:mm:ss} - {(IsSuccessful ? "SUCCESS" : "FAILED")}";
    }
}
