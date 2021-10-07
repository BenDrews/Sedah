using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

[CreateAssetMenu(fileName = "DamageStatus", menuName = "ScriptableObjects/Statuses/DamageStatus")]
public class DamageStatus : StatusData
{
    public DamageType dmg;
    public override void ApplyEffectEOT(CharacterObject target)
    {
        DamageInfo damageInfo = new DamageInfo(_value, target.gameObject, target.transform.position, dmg);
        target.GetComponent<Health>().TakeDamage(damageInfo);
    }

    public override void ApplyEffectStatic(CharacterObject target)
    {
        DamageInfo damageInfo = new DamageInfo(_value, target.gameObject, target.transform.position, dmg);
        target.GetComponent<Health>().TakeDamage(damageInfo);
    }
}
