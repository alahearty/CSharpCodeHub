namespace TextAdventureGame.Core;

// Interface for locations - demonstrates INTERFACE SEGREGATION
public interface ILocation : IGameObject
{
    List<ILocation> ConnectedLocations { get; }
    List<IGameObject> Objects { get; }
    void AddObject(IGameObject obj);
    void RemoveObject(IGameObject obj);
    void ConnectTo(ILocation location);
}
