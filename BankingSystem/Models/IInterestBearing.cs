namespace BankingSystem.Models;

// Interface demonstrating INTERFACE SEGREGATION PRINCIPLE
// Only contains methods related to interest calculation
public interface IInterestBearing
{
    decimal CalculateInterest();
}
