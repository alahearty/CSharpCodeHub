namespace BankingSystem.Models;

// Derived class demonstrating INHERITANCE with business logic
public class BusinessAccount : BankAccount
{
    private readonly decimal _creditLimit;
    private readonly string _businessName;

    public override string AccountType => "Business";
    public decimal CreditLimit => _creditLimit;
    public string BusinessName => _businessName;

    public BusinessAccount(string accountNumber, string businessName, decimal initialBalance, decimal creditLimit)
        : base(accountNumber, businessName, initialBalance)
    {
        if (creditLimit < 0)
            throw new ArgumentException("Credit limit cannot be negative", nameof(creditLimit));
        
        _creditLimit = creditLimit;
        _businessName = businessName;
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

        // Business accounts have higher credit limits
        if (amount <= _balance + _creditLimit)
        {
            _balance -= amount;
            return true;
        }

        return false;
    }

    public override string GetAccountInfo()
    {
        return base.GetAccountInfo() + $" | Credit Limit: ${_creditLimit:F2}";
    }
}
