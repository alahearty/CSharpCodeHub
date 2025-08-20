namespace BankingSystem.Models;

// Derived class demonstrating INHERITANCE
public class SavingsAccount : BankAccount, IInterestBearing
{
    private readonly decimal _interestRate;

    public override string AccountType => "Savings";
    public decimal InterestRate => _interestRate;

    public SavingsAccount(string accountNumber, string accountHolderName, decimal initialBalance, decimal interestRate)
        : base(accountNumber, accountHolderName, initialBalance)
    {
        if (interestRate < 0 || interestRate > 1)
            throw new ArgumentException("Interest rate must be between 0 and 1", nameof(interestRate));
        
        _interestRate = interestRate;
    }

    // Implementation of abstract methods
    public override bool Deposit(decimal amount)
    {
        if (!ValidateAmount(amount))
            return false;

        _balance += amount;
        return true;
    }

    public override bool Withdraw(decimal amount)
    {
        if (!ValidateAmount(amount) || amount > _balance)
            return false;

        _balance -= amount;
        return true;
    }

    // Implementation of interface method
    public decimal CalculateInterest()
    {
        return _balance * _interestRate;
    }

    // Override virtual method from base class
    public override string GetAccountInfo()
    {
        return base.GetAccountInfo() + $" | Interest Rate: {_interestRate:P2}";
    }
}
