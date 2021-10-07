using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public enum StatusType
{
    Burning,
    HealingReduced,
    HealingOverTime,
    Poisoned,
    Stunned,
    Silenced,
    Buff,
    Debuff
}
public abstract class StatusData: ScriptableObject
{
    public int id;
    [SerializeField] protected StatusType statusType;
    [SerializeField] protected float _value;
    [SerializeField] protected float duration;
    [HideInInspector] public double timeApplied;
    [SerializeField] protected bool _static;
    [SerializeField] protected bool eot;
    [SerializeField] protected bool hasStatModifier;
    [SerializeField] protected Sprite image;
    protected CharacterObject owner;

    public StatusType StatusType
    {
        get { return statusType; }
        set { statusType = value; }
    }

    public float Value
    {
        get { return _value; }
        set { _value = value; }
    }

    public float Duration
    {
        get { return duration; }
        set { duration = value; }
    }

    public Sprite Image
    {
        get { return image; }
        set { image = value; }
    }

    public bool Static
    {
        get { return _static; }
        set { _static = value; }
    }

    public bool HasStatModifier
    {
        get { return hasStatModifier; }
        set { hasStatModifier = value; }
    }

    public bool EOT
    {
        get { return eot; }
        set { eot = value; }
    }

    public virtual void ApplyEffectStatic(CharacterObject target)
    {

    }

    public virtual void ApplyEffectEOT(CharacterObject target)
    {

    }

    public virtual StatModifier ApplyModStatic(CharacterObject target)
    {
        return null;
    }

    public virtual StatModifier ApplyModEOT(CharacterObject target)
    {
        return null;
    }

    public virtual void RemoveEffects(CharacterObject target, List<StatModifier> statModifiers)
    {

    }
}
