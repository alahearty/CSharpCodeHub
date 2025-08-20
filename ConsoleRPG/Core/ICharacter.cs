namespace ConsoleRPG.Core;

// Base interface for all characters - demonstrates INTERFACE SEGREGATION
public interface ICharacter
{
    string Name { get; }
    int Level { get; }
    int Health { get; }
    int MaxHealth { get; }
    int Attack { get; }
    int Defense { get; }
    bool IsAlive { get; }
    void TakeDamage(int damage);
    void Heal(int amount);
}
