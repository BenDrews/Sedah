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
public class Status
{
    [SerializeField] private StatusType statusType;
    [SerializeField] private float _value;
    [SerializeField] private float duration;
    [HideInInspector] public double timeApplied;

    public Status(StatusEffect statusEffect)
    {
        statusType = statusEffect.statusType;
        _value = statusEffect.value;
        duration = statusEffect.duration;
        timeApplied = NetworkTime.time;
    }
    public Status(StatusType statusType, float _value, float duration)
    {
        this.statusType = statusType;
        this._value = _value;
        this.duration = duration;
        timeApplied = NetworkTime.time;
    }

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
}
