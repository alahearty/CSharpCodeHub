namespace TextAdventureGame.Commands;

using TextAdventureGame.Entities;

// Command interface demonstrating COMMAND PATTERN
public interface ICommand
{
    string Name { get; }
    string Description { get; }
    bool CanExecute(Player player);
    void Execute(Player player, string[] parameters);
}
