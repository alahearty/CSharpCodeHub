namespace BankingSystem.Models;

// Derived class demonstrating INHERITANCE with different behavior
public class CheckingAccount : BankAccount
{
    private readonly decimal _overdraftLimit;

    public override string AccountType => "Checking";
    public decimal OverdraftLimit => _overdraftLimit;

    public CheckingAccount(string accountNumber, string accountHolderName, decimal initialBalance, decimal overdraftLimit)
        : base(accountNumber, accountHolderName, initialBalance)
    {
        if (overdraftLimit < 0)
            throw new ArgumentException("Overdraft limit cannot be negative", nameof(overdraftLimit));
        
        _overdraftLimit = overdraftLimit;
    }

    public override bool Deposit(decimal amount)
    {
        if (!ValidateAmount(amount))
            return false;

        _balance += amount;
        return true;
    }

    public override bool Withdraw(decimal amount)
    {
        if (!ValidateAmount(amount))
            return false;

        // Allow withdrawal up to overdraft limit
        if (amount <= _balance + _overdraftLimit)
        {
            _balance -= amount;
            return true;
        }

        return false;
    }

    public override string GetAccountInfo()
    {
        return base.GetAccountInfo() + $" | Overdraft Limit: ${_overdraftLimit:F2}";
    }
}
