namespace ConsoleRPG.Items;

// Base interface for all items - demonstrates INTERFACE SEGREGATION
public interface IItem
{
    string Name { get; }
    string Description { get; }
}

