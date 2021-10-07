using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "BuffStatus", menuName = "ScriptableObjects/Statuses/BuffStatus")]
public class BuffStatus : StatusData
{
    public StatType statTypeStatic;
    public StatModType statModTypeStatic;
    public int orderStatic;

    public StatType statTypeEOT;
    public StatModType statModTypeEOT;
    public int orderEOT;

    // Apply StatModifiers every second of the status duration.
    public override StatModifier ApplyModEOT(CharacterObject target)
    {

        StatModifier statModifier = new StatModifier(_value, statModTypeEOT, orderEOT, this);
        target.AddStatModifier(statModifier, statTypeEOT);
        return statModifier;
    }

    // Apply StatModifiers at the start of the status.
    public override StatModifier ApplyModStatic(CharacterObject target)
    {
        StatModifier statModifier = new StatModifier(_value, statModTypeStatic, orderStatic, this);
        target.AddStatModifier(statModifier, statTypeStatic);
        return statModifier;
    }

    public override void RemoveEffects(CharacterObject target, List<StatModifier> statModifiers)
    {
        foreach (StatModifier mod in statModifiers)
        {
            if (eot)
            {
                target.RemoveStatModifier(mod, statTypeEOT);
            }
            if (_static)
            {
                target.RemoveStatModifier(mod, statTypeStatic);
            }

        }
    }
}

