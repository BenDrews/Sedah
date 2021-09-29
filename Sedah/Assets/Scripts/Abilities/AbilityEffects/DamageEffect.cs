using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageEffect : AbilityEffect
{

    public DamageType damageType;
    public float damageAmount;

    public DamageEffect(DamageEffectData data)
    {
        damageType = data.damageType;
        damageAmount = data.damageAmount;
    }
    public override void Invoke(CharacterObject owner, CharacterObject target)
    {
        DamageInfo damageInfo = new DamageInfo(damageAmount, owner.gameObject, target.transform.position, damageType);
        target.GetComponent<Health>().TakeDamage(damageInfo);
    }

}
