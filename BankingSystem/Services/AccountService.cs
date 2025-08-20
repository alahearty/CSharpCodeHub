namespace BankingSystem.Services;

// Service demonstrating SINGLE RESPONSIBILITY PRINCIPLE
// This service only handles account-related operations
public class AccountService
{
    public void UpdateAccountInfo(BankAccount account, string newHolderName)
    {
        if (account == null)
            throw new ArgumentNullException(nameof(account));
        
        if (string.IsNullOrWhiteSpace(newHolderName))
            throw new ArgumentException("Account holder name cannot be empty", nameof(newHolderName));

        // Use reflection to update the private field (in real scenario, this would be a public method)
        var field = typeof(BankAccount).GetField("_accountHolderName", 
            System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
        
        if (field != null)
        {
            field.SetValue(account, newHolderName);
            Console.WriteLine($"Account holder name updated to: {newHolderName}");
        }
    }

    public bool ValidateAccount(BankAccount account)
    {
        if (account == null)
            return false;

        return !string.IsNullOrEmpty(account.AccountNumber) && 
               !string.IsNullOrEmpty(account.AccountHolderName) && 
               account.Balance >= 0;
    }

    public string GetAccountSummary(BankAccount account)
    {
        if (account == null)
            return "Invalid account";

        return $"Account {account.AccountNumber} ({account.AccountType}) - {account.AccountHolderName}";
    }
}
