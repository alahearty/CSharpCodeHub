namespace TextAdventureGame.Core;

// Concrete location class demonstrating OPEN/CLOSED PRINCIPLE
public class Location : ILocation
{
    public string Id { get; }
    public string Name { get; }
    public string Description { get; }
    public bool IsActive { get; set; }
    public List<ILocation> ConnectedLocations { get; }
    public List<IGameObject> Objects { get; }

    public Location(string id, string name, string description)
    {
        Id = id;
        Name = name;
        Description = description;
        IsActive = true;
        ConnectedLocations = new List<ILocation>();
        Objects = new List<IGameObject>();
    }

    public void AddObject(IGameObject obj)
    {
        if (obj != null && !Objects.Contains(obj))
        {
            Objects.Add(obj);
        }
    }

    public void RemoveObject(IGameObject obj)
    {
        if (obj != null)
        {
            Objects.Remove(obj);
        }
    }

    public void ConnectTo(ILocation location)
    {
        if (location != null && !ConnectedLocations.Contains(location))
        {
            ConnectedLocations.Add(location);
            // Bidirectional connection
            if (location is Location concreteLocation)
            {
                concreteLocation.ConnectedLocations.Add(this);
            }
        }
    }

    public override string ToString()
    {
        return $"{Name}: {Description}";
    }
}
