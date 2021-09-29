using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "StatusEffect", menuName = "ScriptableObjects/AbilityEffects/StatusEffect")]
public class StatusEffectData : AbilityEffectData
{
    public StatusType statusType;
    public float value;
    public float duration;

    public StatusEffectData()
    {
        type = typeof(StatusEffect).AssemblyQualifiedName;
    }
}
