using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "StatusEffect", menuName = "ScriptableObjects/AbilityEffects/StatusEffect")]
public class StatusEffectData : AbilityEffectData
{
    public Status status;

    // choose to affect target or owner.
    public bool affectSelf;

    public StatusEffectData()
    {
        type = typeof(StatusEffect).AssemblyQualifiedName;
    }
}
