using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using Mirror;

[RequireComponent(typeof(EntityStateMachine))]
public class CharacterObject : NetworkBehaviour
{
    [SerializeField] private AbilityDatabase AbilityDatabase;
    [SerializeField] private Character character;

    //Stats for the PlayerCharacter
    private Dictionary<StatType, Stat> CharacterStats = new Dictionary<StatType, Stat>();

    //Status Effects for the PlayerCharacter
    private readonly Dictionary<StatusType, List<Status>> StatusEffects = new Dictionary<StatusType, List<Status>>();
    [SerializeField]private readonly List<GameObject> Abilities = new List<GameObject>();

    private int Gold = 0;
    private int Level = 1;

    private EntityStateMachine stateMachine;
    
    public override void OnStartServer()
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
        CharacterStats.Add(StatType.AttackRange, new Stat(character.AttackRange, 0, StatType.AttackRange));

        List<int> abilities = character.GetAbilities();
        foreach (int a in abilities)
        {
            GameObject obj = Instantiate(AbilityDatabase.GetAbility(a), transform);
            NetworkServer.Spawn(obj);
            Abilities.Add(obj);
        }

        stateMachine = GetComponent<EntityStateMachine>();
        stateMachine.SetNextState(new IdlingState(stateMachine));
    }

    // Update is called once per frame
    void Update()
    {

    }

    public CharacterObject()
    {

    }
    

    public CharacterObject(Character character)
    {
        this.character = character;
    }

    public Stat GetStat(StatType sType)
    {
        return CharacterStats[sType];
    }

    public List<Status> GetStatus(StatusType stType)
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

    public float GetTotalStatusValue(StatusType stType)
    {
        return HasStatus(stType) ? StatusEffects[stType].Select(x => x.Value).Sum() : 0f;
    }

    public float GetMaxStatusValue(StatusType stType)
    {
        return HasStatus(stType) ? StatusEffects[stType].Select(x => x.Value).Max() : 0f;
    }

    public void AddStatus(Status status)
    {
        if (!StatusEffects.ContainsKey(status.StatusType))
        {
            StatusEffects.Add(status.StatusType, new List<Status>());
        }
        StatusEffects[status.StatusType].Add(status);
    }

    public GameObject GetAbility(int i)
    {
        return Abilities[i];
    }

    public void AddAbility(int i)
    {
        //Abilities.Add(AbilityDatabase.GetAbility(i));
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
