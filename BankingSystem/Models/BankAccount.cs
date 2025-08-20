namespace BankingSystem.Models;

// Abstract base class demonstrating ABSTRACTION
public abstract class BankAccount
{
    // Protected fields for inheritance
    protected decimal _balance;
    protected readonly string _accountNumber;
    protected string _accountHolderName;
    protected readonly DateTime _createdDate;

    // Properties with encapsulation
    public string AccountNumber => _accountNumber;
    public string AccountHolderName => _accountHolderName;
    public decimal Balance => _balance;
    public DateTime CreatedDate => _createdDate;
    public abstract string AccountType { get; }

    protected BankAccount(string accountNumber, string accountHolderName, decimal initialBalance)
    {
        _accountNumber = accountNumber ?? throw new ArgumentNullException(nameof(accountNumber));
        _accountHolderName = accountHolderName ?? throw new ArgumentNullException(nameof(accountHolderName));
        _createdDate = DateTime.Now;
        
        if (initialBalance < 0)
            throw new ArgumentException("Initial balance cannot be negative", nameof(initialBalance));
        
        _balance = initialBalance;
    }

    // Abstract methods that must be implemented by derived classes
    public abstract bool Withdraw(decimal amount);
    public abstract bool Deposit(decimal amount);
    
    // Virtual method that can be overridden
    public virtual string GetAccountInfo()
    {
        return $"Account: {_accountNumber} | Holder: {_accountHolderName} | Balance: ${_balance:F2} | Type: {AccountType}";
    }

    // Protected method for derived classes to use
    protected bool ValidateAmount(decimal amount)
    {
        return amount > 0;
    }
}
