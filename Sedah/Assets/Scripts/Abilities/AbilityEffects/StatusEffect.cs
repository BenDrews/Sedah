using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatusEffect : AbilityEffect
{
    public Status status;
    public bool affectSelf;

    public StatusEffect(StatusEffectData data)
    {
        status = data.status;
        affectSelf = data.affectSelf;
    }
    public override void Invoke(CharacterObject owner, CharacterObject target)
    {
        if (affectSelf)
        {
            owner.AddStatus(status);
        }
        else
        {
            target.AddStatus(status);
        }
    }
}
