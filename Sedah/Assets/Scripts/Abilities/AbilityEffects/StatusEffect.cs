using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatusEffect : AbilityEffect
{
    public StatusData status;
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
            owner.AddStatus(new Status(status, owner, owner, false));
        }
        else
        {
            target.AddStatus(new Status(status, owner, target, false));
        }
    }
}
