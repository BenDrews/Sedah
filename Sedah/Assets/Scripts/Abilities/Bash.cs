using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
[System.Serializable]
public class Bash : Ability
{
    [SerializeField] protected GameObject specialEffect;
    public override void Activate(GameObject attacker, GameObject target)
    {
        DamageInfo dmgInfo = new DamageInfo(damage, attacker, target.transform.position, DamageType.Physical);
        target.GetComponent<Health>().TakeDamage(dmgInfo);
        GameObject effect = Instantiate(specialEffect);
        effect.transform.position = attacker.transform.position;
        NetworkServer.Spawn(effect);
        StartCoroutine(DestroyEffect(effect));

    }

    public IEnumerator DestroyEffect(GameObject effect)
    {
        yield return new WaitForSeconds(2f);
        Destroy(effect);
    }
}
