namespace ConsoleRPG.Core;

using ConsoleRPG.Skills;

// Interface for characters that can participate in combat
public interface ICombatant : ICharacter
{
    List<ISkill> Skills { get; }
    new void Attack(ICombatant target);
    void UseSkill(ISkill skill, ICombatant target);
    void GainExperience(int amount);
    void LevelUp();
}
