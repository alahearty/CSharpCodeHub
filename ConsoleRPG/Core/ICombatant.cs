namespace ConsoleRPG.Core;

// Interface for characters that can participate in combat
public interface ICombatant : ICharacter
{
    List<ISkill> Skills { get; }
    void Attack(ICombatant target);
    void UseSkill(ISkill skill, ICombatant target);
    void GainExperience(int amount);
    void LevelUp();
}
