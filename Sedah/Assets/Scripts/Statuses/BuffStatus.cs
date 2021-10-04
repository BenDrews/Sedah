using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "BuffStatus", menuName = "ScriptableObjects/Statuses/BuffStatus")]
public class BuffStatus : Status
{
    public StatType statType;
    public StatModType statModType;
    public int order;
    private StatModifier statModifier;
    private List<StatModifier> statModifiers = new List<StatModifier>();
    public override void ApplyEffectEOT(CharacterObject target)
    {

        statModifier = new StatModifier(_value, statModType, order, this);
        target.AddStatModifier(statModifier, statType);
        statModifiers.Add(statModifier);
    }

    public override void ApplyEffectStatic(CharacterObject target)
    {
        statModifier = new StatModifier(_value, statModType, order, this);
        target.AddStatModifier(statModifier, statType);
    }

    public override void RemoveEffects(CharacterObject target)
    {
        target.RemoveStatModifier(statModifier, statType);
        if (statModifiers.Count > 0)
        {
            foreach (StatModifier mod in statModifiers)
            {
                target.RemoveStatModifier(mod, statType);
            }
        }
    }
}

