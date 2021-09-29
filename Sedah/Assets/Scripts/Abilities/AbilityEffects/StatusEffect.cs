using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatusEffect : AbilityEffect
{
    public StatusType statusType;
    public float value;
    public float duration;

    public StatusEffect(StatusEffectData data)
    {
        statusType = data.statusType;
        value = data.value;
        duration = data.duration;
    }
    public override void Invoke(CharacterObject owner, CharacterObject target)
    {
        target.AddStatus(new Status(this));
    }
}
