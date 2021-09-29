using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "DamageEffect", menuName = "ScriptableObjects/AbilityEffects/DamageEffect")]
public class DamageEffectData : AbilityEffectData
{
    public float damageAmount;
    public DamageType damageType;

    public DamageEffectData()
    {
        type = typeof(DamageEffect).AssemblyQualifiedName;
    }
}
