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
    ArmorBuff,
    MRBuff
}
public class Status: ScriptableObject
{
    [SerializeField] protected StatusType statusType;
    [SerializeField] protected float _value;
    [SerializeField] protected float duration;
    [HideInInspector] public double timeApplied;
    private bool elapsed = false;
    [SerializeField] protected bool _static;
    [SerializeField] protected bool eot;
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

    public bool HasElapsed()
    {
        return elapsed;
    }

    public bool Static()
    {
        return _static;
    }

    public bool EOT()
    {
        return eot;
    }

    public IEnumerator DurationElapsed(CharacterObject target)
    {
        if (_static)
        {
            ApplyEffectStatic(target);
        }
        for (int i = 0; i < duration; i++)
        {
            yield return new WaitForSeconds(1f);
            if (eot)
            {
                ApplyEffectEOT(target);
            }
        }
        RemoveEffects(target);
        elapsed = true;
    }

    public virtual void ApplyEffectStatic(CharacterObject target)
    {

    }

    public virtual void ApplyEffectEOT(CharacterObject target)
    {

    }

    public virtual void RemoveEffects(CharacterObject target)
    {

    }
}
