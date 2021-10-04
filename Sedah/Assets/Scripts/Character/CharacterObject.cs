using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using Mirror;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(EntityStateMachine))]
public class CharacterObject : NetworkBehaviour
{
    [SerializeField] private AbilityDatabase abilityDatabase;
    [SerializeField] private Character character;    

    //Stats for the PlayerCharacter
    private Dictionary<StatType, Stat> characterStats = new Dictionary<StatType, Stat>();

    //Status Effects for the PlayerCharacter
    private readonly Dictionary<StatusType, List<Status>> statusEffects = new Dictionary<StatusType, List<Status>>();
    [SerializeField]private readonly List<GameObject> abilities = new List<GameObject>();
    [SerializeField] private GameObject abilityTemplate;

    private int gold = 0;
    private int level = 1;
    private bool finishedLoading = false;
    private EntityStateMachine stateMachine;
    private Animator animator;

    public Team team = Team.Neutral;

    public void Awake()
    {
        Debug.Log("STATS LOADED");
        characterStats.Add(StatType.Health, new Stat(character.Health, character.HealthPerLvl, StatType.Health));
        characterStats.Add(StatType.Mana, new Stat(character.Mana, character.ManaPerLvl, StatType.Mana));
        characterStats.Add(StatType.AttackDamage, new Stat(character.AttackDamage, character.ADPerLvl, StatType.AttackDamage));
        characterStats.Add(StatType.AbilityPower, new Stat(0, 0, StatType.Health));
        characterStats.Add(StatType.AttackSpeed, new Stat(character.AttackSpeed, character.ASPerLvl, StatType.Health));
        characterStats.Add(StatType.MoveSpeed, new Stat(character.MoveSpeed, 0, StatType.MoveSpeed));
        characterStats.Add(StatType.CDR, new Stat(0, 0, StatType.CDR));
        characterStats.Add(StatType.Armor, new Stat(character.Armor, character.ArmorPerLvl, StatType.Armor));
        characterStats.Add(StatType.MagicRes, new Stat(character.MagicRes, character.MRPerLvl, StatType.MagicRes));
        characterStats.Add(StatType.ArmPen, new Stat(0, 0, StatType.ArmPen));
        characterStats.Add(StatType.MagicPen, new Stat(0, 0, StatType.MagicPen));
        characterStats.Add(StatType.AttackRange, new Stat(character.AttackRange, 0, StatType.AttackRange));

        finishedLoading = true;
    }

    public override void OnStartServer()
    {
        base.OnStartServer();
        stateMachine = GetComponent<EntityStateMachine>();
        stateMachine.SetNextState(new IdlingState(stateMachine));
        List<int> abilityIds = character.GetAbilities();
        foreach (int a in abilityIds)
        {
            GameObject obj = Instantiate(abilityTemplate, transform);
            NetworkServer.Spawn(obj);
            obj.GetComponent<AbilityTemplate>().Initialize(abilityDatabase.GetAbility(a));
            abilities.Add(obj);
            RpcAbilitySetup(obj, a);
        }
        animator = GetComponent<Animator>();
    }

    [ClientRpc]

    public void RpcAbilitySetup(GameObject obj, int i)
    {
        Debug.Log(abilityDatabase);
        Debug.Log(abilityDatabase.GetAbility(i));
        obj.GetComponent<AbilityTemplate>().Initialize(abilityDatabase.GetAbility(i));
        abilities.Add(obj);
    }

    public void Update()
    {
        if (isServer)
        {
            List<StatusType> removeStatus1 = new List<StatusType>();
            foreach (KeyValuePair<StatusType, List<Status>> entry in statusEffects)
            {
                List<Status> removeStatus2 = new List<Status>();
                foreach (Status status in entry.Value)
                {
                    if (status.HasElapsed())
                    {
                        removeStatus2.Add(status);
                    }
                }
                foreach (Status status in removeStatus2)
                {
                    entry.Value.Remove(status);
                }
                if (entry.Value.Count == 0)
                {
                    removeStatus1.Add(entry.Key);
                }
            }
            foreach (StatusType status in removeStatus1)
            {
                statusEffects.Remove(status);
            }
        }
    }
    public CharacterObject()
    {

    }
    
    public bool HasFinishedLoading()
    {
        return finishedLoading;
    }
    public CharacterObject(Character character)
    {
        this.character = character;
    }

    public Stat GetStat(StatType sType)
    {
        return characterStats[sType];
    }

    public List<Status> GetStatus(StatusType stType)
    {
        return statusEffects[stType];
    }

    public bool HasStat(StatType sType)
    {
        return characterStats.ContainsKey(sType);
    }

    public bool HasStatus(StatusType stType)
    {
        return statusEffects.ContainsKey(stType);
    }

    public float GetStatValue(StatType sType)
    {
        return HasStat(sType) ? characterStats[sType].Value : 0f;
    }

    public float GetTotalStatusValue(StatusType stType)
    {
        return HasStatus(stType) ? statusEffects[stType].Select(x => x.Value).Sum() : 0f;
    }

    public float GetMaxStatusValue(StatusType stType)
    {
        return HasStatus(stType) ? statusEffects[stType].Select(x => x.Value).Max() : 0f;
    }

    public void AddStatus(Status status)
    {
        if (!statusEffects.ContainsKey(status.StatusType))
        {
            statusEffects.Add(status.StatusType, new List<Status>());
        }
        StartCoroutine(status.DurationElapsed(this));
        statusEffects[status.StatusType].Add(status);
    }

    public AbilityTemplate GetAbility(int i)
    {
        if (abilities.Count() <= i)
        {
            return null;
        }
        return abilities[i].GetComponent<AbilityTemplate>();
    }

    public void AddAbility(int i)
    {
        //Abilities.Add(AbilityDatabase.GetAbility(i));
    }

    public void AddStatModifier(StatModifier statModifier, StatType sType)
    {
        characterStats[sType].AddModifier(statModifier);
    }

    public void RemoveStatModifier(StatModifier statModifier, StatType sType)
    {
        characterStats[sType].RemoveModifier(statModifier);
    }
    

    public void AddGold(int g)
    {
        gold += g;
    }

    public void RemoveGold(int g)
    {
        gold -= g;
    }

    public void LevelUp()
    {
        level++;
    }
}
