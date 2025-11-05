namespace ConsoleRPG.Skills;

using ConsoleRPG.Core;

// Interface for skills - demonstrates INTERFACE SEGREGATION
public interface ISkill
{
    string Name { get; }
    string Description { get; }
    int Cost { get; }
    void Use(ICombatant user, ICombatant target);
}

