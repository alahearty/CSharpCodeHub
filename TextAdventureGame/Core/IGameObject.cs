namespace TextAdventureGame.Core;

// Base interface for all game objects - demonstrates INTERFACE SEGREGATION
public interface IGameObject
{
    string Id { get; }
    string Name { get; }
    string Description { get; }
    bool IsActive { get; set; }
}
