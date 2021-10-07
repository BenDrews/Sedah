using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
[System.Serializable]

[CreateAssetMenu(fileName = "TargetAbility", menuName = "ScriptableObjects/Abilities/TargetAbility")]
public class TargetAbility : AbilityData
{
    public override void Activate(CharacterObject attacker, CharacterObject target, AbilityEffect[] abilityEffects)
    {
        foreach (AbilityEffect abilityEffect in abilityEffects)
        {
            abilityEffect.Invoke(attacker, target);
        }
    }
}
