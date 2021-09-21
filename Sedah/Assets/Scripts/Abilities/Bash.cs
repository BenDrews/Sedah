using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
[System.Serializable]
public class Bash : Ability
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override void Activate(GameObject attacker, GameObject target)
    {
        if (!OnCooldown)
        {
            RunCd = StartCoroutine(RunCooldown());
            Ability(attacker, target);
        }
    }

    private void Ability(GameObject attacker, GameObject target)
    {
        float damage = Damage;
        DamageInfo dmgInfo = new DamageInfo(damage, attacker, target.transform.position, DamageType.Physical);
        target.GetComponent<Health>().TakeDamage(dmgInfo);
    }
}
