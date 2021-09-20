using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Collections.ObjectModel;

public enum StatType
{
    Health,
    Mana,
    AttackDamage,
    AbilityPower,
    AttackSpeed,
    MoveSpeed,
    CDR,
    Armor,
    MagicRes,
    ArmPen,
    MagicPen,
    HealBuff,
    DamageBuff,
    DamageReduction,
    AttackRange
}

[Serializable]
public class Stat
{
    public float BaseValue;
    public float ValuePerLevel;
    public float lastBaseValue = float.MinValue;
    //isDirty is to prevent recalculating finalStatValue constantly.
    public bool isDirty = true;
    public float _value;

    public readonly List<StatModifier> statModifiers = new List<StatModifier>();
    public readonly List<StatModifier> StatModifiers;
    [SerializeField] private StatType SType;

    //constructor without a base value.
    public Stat()
    {
    }

    //constructor with a base value.
    public Stat(float baseValue, float valuePerLevel, StatType statType) : this()
    {
        BaseValue = baseValue;
        ValuePerLevel = valuePerLevel;
        SType = statType;

    }

    //modify value when level up.Re
    public void LevelUp(int level)
    {
        BaseValue += level * ValuePerLevel;
    }

    // Change Value
    public virtual float Value
    {
        get
        {
            if (isDirty || lastBaseValue != BaseValue)
            {
                lastBaseValue = BaseValue;
                _value = CalculateFinalValue();
                isDirty = false;
            }
            return _value;
        }
    }

    public virtual void AddModifier(StatModifier mod)
    {
        isDirty = true;
        statModifiers.Add(mod);
        statModifiers.Sort(CompareModifierOrder);
    }

    protected virtual int CompareModifierOrder(StatModifier a, StatModifier b)
    {
        if (a.Order < b.Order)
            return -1;
        else if (a.Order > b.Order)
            return 1;
        return 0; // if (a.Order == b.Order)
    }

    public virtual bool RemoveModifier(StatModifier mod)
    {
        isDirty = true;
        return statModifiers.Remove(mod);
    }
    public virtual bool RemoveAllModifiersFromSource(object source)
    {
        bool didRemove = false;

        for (int i = statModifiers.Count - 1; i >= 0; i--)
        {
            if (statModifiers[i].Source == source)
            {
                isDirty = true;
                didRemove = true;
                statModifiers.RemoveAt(i);
            }
        }
        return didRemove;
    }

    protected virtual float CalculateFinalValue()
    {
        float finalValue = BaseValue;
        float sumPercentAdd = 0; // This will hold the sum of our "PercentAdd" modifiers

        for (int i = 0; i < statModifiers.Count; i++)
        {
            StatModifier mod = statModifiers[i];

            if (mod.Type == StatModType.Flat)
            {
                finalValue += mod.Value;
            }
            else if (mod.Type == StatModType.PercentAdd) // When we encounter a "PercentAdd" modifier
            {
                sumPercentAdd += mod.Value; // Start adding together all modifiers of this type

                // If we're at the end of the list OR the next modifer isn't of this type
                if (i + 1 >= statModifiers.Count || statModifiers[i + 1].Type != StatModType.PercentAdd)
                {
                    finalValue *= 1 + sumPercentAdd; // Multiply the sum with the "finalValue", like we do for "PercentMult" modifiers
                    sumPercentAdd = 0; // Reset the sum back to 0
                }
            }
            else if (mod.Type == StatModType.PercentMult) // Percent renamed to PercentMult
            {
                finalValue *= 1 + mod.Value;
            }
        }

        return (float)Math.Round(finalValue, 4);
    }
}
