using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class Status
{
    public int id;
    private Sprite image;
    private StatusType statusType;
    private float _value;
    private float duration;
    public double timeApplied;
    private bool elapsed = false;
    private bool _static;
    private bool eot;
    private bool hasStatModifier;
    private bool isDummy;
    private StatusData data;
    private CharacterObject owner;
    private CharacterObject target;
    private StatModifier statModifier;
    private List<StatModifier> statModifiers = new List<StatModifier>();
    public Status(StatusData data, CharacterObject owner, CharacterObject target, bool isDummy)
    {
        this.data = data;
        id = data.id;
        _value = data.Value;
        duration = data.Duration;
        _static = data.Static;
        eot = data.EOT;
        hasStatModifier = data.HasStatModifier;
        this.owner = owner;
        this.target = target;
        this.isDummy = isDummy;
        image = data.Image;

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

    public Sprite Image
    {
        get { return image; }
        set { image = value; }
    }

    public float Duration
    {
        get { return duration; }
        set { duration = value; }
    }

    public bool Static
    {
        get { return _static; }
        set { _static = value; }
    }

    public bool EOT
    {
        get { return eot; }
        set { eot = value; }
    }

    public bool HasElapsed()
    {
        return elapsed;
    }

    public IEnumerator DurationElapsed(CharacterObject target)
    {
        elapsed = false;
        if (_static && !isDummy)
        {
            if (hasStatModifier)
            {
                statModifiers.Add(data.ApplyModStatic(target));
            }
            else
            {
                data.ApplyEffectStatic(target);
            }
        }
        for (int i = 0; i < duration; i++)
        {
            yield return new WaitForSeconds(1f);
            if (eot && !isDummy)
            {
                if (hasStatModifier)
                {
                    statModifiers.Add(data.ApplyModEOT(target));
                }
                else
                {
                    data.ApplyEffectEOT(target);
                }
            }
        }
        if (statModifiers.Count > 0 && !isDummy)
        {
            data.RemoveEffects(target, statModifiers);
        }
        elapsed = true;
    }
}
