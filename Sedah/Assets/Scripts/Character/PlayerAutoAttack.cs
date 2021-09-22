using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sedah;
using Mirror;

[RequireComponent(typeof(CharacterObject))]
[RequireComponent(typeof(EntityStateMachine))]
public class PlayerAutoAttack : NetworkBehaviour
{
    private CharacterObject character;
    public GameObject target;
    [SyncVar]
    private double timeLastAttacked;

    [Command]
    private void CmdSetAutoAttackState(GameObject target)
    {
        EntityStateMachine stateMachine = GetComponent<EntityStateMachine>();
        stateMachine.SetNextState(new AutoAttackingState(stateMachine));
        this.target = target;

    }

    private void AutoAttack(GameObject target)
    {
        EntityStateMachine stateMachine = GetComponent<EntityStateMachine>();
        if (stateMachine.GetState().type == EntityStateType.AutoAttacking)
        {
            // Check if target is in range
            float dist = Vector3.Distance(transform.position, target.transform.position);
            if (dist <= this.character.GetStatValue(StatType.AttackRange))
            {
                float damage = character.GetStatValue(StatType.AttackDamage);
                float attackSpeed = character.GetStatValue(StatType.AttackSpeed);
                DamageInfo dmgInfo = new DamageInfo(damage, gameObject, target.transform.position, DamageType.Physical);
                target.GetComponent<Health>().TakeDamage(dmgInfo);
                timeLastAttacked = NetworkTime.time;
            }
            else
            {
                this.GetComponent<PlayerMovement>()?.CmdMove(target.transform.position);
            }
        }
    }

    // This script should only exist on the local player
    private void Start()
    {
        if (NetworkClient.localPlayer.netId == base.netId)
        {
            this.character = GetComponent<CharacterObject>();
        }
        else
        {
            Destroy(this);
        }
    }

    // Update is called once per frame
    void Update()
    {
        EntityStateMachine stateMachine = GetComponent<EntityStateMachine>();
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
                    CmdSetAutoAttackState(obj);
                }
            }
        }

        if (NetworkServer.active)
        {
            AutoAttack(target);
        }
    }
}
