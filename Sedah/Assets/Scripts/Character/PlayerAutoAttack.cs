using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

[RequireComponent(typeof(CharacterObject))]
public class PlayerAutoAttack : NetworkBehaviour
{
    private CharacterObject character;
    [SyncVar]
    private double timeLastAttacked;
    [Command]
    private void CmdAutoAttack(GameObject target)
    {
        float damage = character.GetStatValue(StatType.AttackDamage);
        float attackSpeed = character.GetStatValue(StatType.AttackSpeed);
        DamageInfo dmgInfo = new DamageInfo(damage, gameObject, target.transform.position, DamageType.Physical);
        target.GetComponent<Health>().TakeDamage(dmgInfo);
        timeLastAttacked = NetworkTime.time;
    }

    private void Start()
    {
        if (NetworkClient.localPlayer.netId == base.netId)
        {
            this.character = base.GetComponent<CharacterObject>();
        } else
        {
            Destroy(this);
        }

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            LayerMask layerMask = LayerMask.GetMask("TargetableCharacters");
            if (Physics.Raycast(ray, out hit, Mathf.Infinity, layerMask))
            {
                GameObject obj = hit.transform.gameObject;
                if (obj.GetComponent<Health>() != null)
                {
                    float dist = Vector3.Distance(transform.position, obj.transform.position);
                    if (dist <= this.character.GetStatValue(StatType.AttackRange))
                    {
                        CmdAutoAttack(obj);
                    }
                }
            }
        }
    }
}
