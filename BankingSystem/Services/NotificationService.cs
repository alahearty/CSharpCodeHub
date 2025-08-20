namespace BankingSystem.Services;

using BankingSystem.Models;

// Service demonstrating SINGLE RESPONSIBILITY PRINCIPLE
// This service only handles notification-related operations
public class NotificationService
{
    public void SendTransactionNotification(Transaction transaction)
    {
        if (transaction == null)
            return;

        var message = transaction.IsSuccessful
            ? $"✅ Transaction {transaction.TransactionId} completed successfully: {transaction.Type} of ${transaction.Amount:F2}"
            : $"❌ Transaction {transaction.TransactionId} failed: {transaction.Type} of ${transaction.Amount:F2}";

        Console.WriteLine($"📧 Notification: {message}");
        
        // In a real application, this would send emails, SMS, push notifications, etc.
        // For demonstration, we just print to console
    }

    public void SendAccountAlert(BankAccount account, string alertType, string message)
    {
        if (account == null)
            return;

        var alert = $"[{alertType.ToUpper()}] Account {account.AccountNumber}: {message}";
        Console.WriteLine($"🚨 Alert: {alert}");
    }

    public void SendLowBalanceWarning(BankAccount account)
    {
        if (account == null || account.Balance > 100)
            return;

        var warning = $"⚠️ Low balance warning for account {account.AccountNumber}. Current balance: ${account.Balance:F2}";
        Console.WriteLine($"📱 SMS Alert: {warning}");
    }
}
