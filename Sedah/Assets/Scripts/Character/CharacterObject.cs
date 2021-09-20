using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class CharacterObject : NetworkBehaviour
{
    [SerializeField] private AbilityDatabase AbilityDatabase;
    
    //Stats for the PlayerCharacter
    private Dictionary<StatType, Stat> CharacterStats = new Dictionary<StatType, Stat>();

    //Status Effects for the PlayerCharacter
    private Dictionary<StatusType, Status> StatusEffects = new Dictionary<StatusType, Status>();
    private List<Ability> Abilities = new List<Ability>();
    private int Gold = 0;
    private int Level = 1;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public CharacterObject(Character character)
    {
        CharacterStats.Add(StatType.Health, new Stat(character.Health, character.HealthPerLvl, StatType.Health));
        CharacterStats.Add(StatType.Mana, new Stat(character.Mana, character.ManaPerLvl, StatType.Mana));
        CharacterStats.Add(StatType.AttackDamage, new Stat(character.AttackDamage, character.ADPerLvl, StatType.AttackDamage));
        CharacterStats.Add(StatType.AbilityPower, new Stat(0, 0, StatType.Health));
        CharacterStats.Add(StatType.AttackSpeed, new Stat(character.AttackSpeed, character.ASPerLvl, StatType.Health));
        CharacterStats.Add(StatType.MoveSpeed, new Stat(character.MoveSpeed, 0, StatType.MoveSpeed));
        CharacterStats.Add(StatType.CDR, new Stat(0, 0, StatType.CDR));
        CharacterStats.Add(StatType.Armor, new Stat(character.Armor, character.ArmorPerLvl, StatType.Armor));
        CharacterStats.Add(StatType.MagicRes, new Stat(character.MagicRes, character.MRPerLvl, StatType.MagicRes));
        CharacterStats.Add(StatType.ArmPen, new Stat(0, 0, StatType.ArmPen));
        CharacterStats.Add(StatType.MagicPen, new Stat(0, 0, StatType.MagicPen));

        List<int> abilities = character.GetAbilities();
        foreach (int a in abilities)
        {
            Abilities.Add(AbilityDatabase.GetAbility(a));
        }

    }

    public Stat GetStat(StatType sType)
    {
        return CharacterStats[sType];
    }

    public Status GetStatus(StatusType stType)
    {
        return StatusEffects[stType];
    }

    public bool HasStat(StatType sType)
    {
        return CharacterStats.ContainsKey(sType);
    }

    public bool HasStatus(StatusType stType)
    {
        return StatusEffects.ContainsKey(stType);
    }

    public float GetStatValue(StatType sType)
    {
        return HasStat(sType) ? CharacterStats[sType].Value : 0f;
    }

    public float GetStatusValue(StatusType stType)
    {
        return HasStatus(stType) ? StatusEffects[stType].Value : 0f;
    }

    public void AddStatus(Status status)
    {
        StatusEffects.Add(status.StatusType, status);
    }

    public Ability GetAbility(int i)
    {
        return Abilities[i];
    }

    public void AddAbility(int i)
    {
        Abilities.Add(AbilityDatabase.GetAbility(i));
    }

    public void AddStatModifier(StatModifier statModifier, StatType sType)
    {
        CharacterStats[sType].AddModifier(statModifier);
    }

    public void RemoveStatModifier(StatModifier statModifier, StatType sType)
    {
        CharacterStats[sType].RemoveModifier(statModifier);
    }

    public void AddGold(int g)
    {
        Gold += g;
    }

    public void RemoveGold(int g)
    {
        Gold -= g;
    }

    public void LevelUp()
    {
        Level++;
    }
}
