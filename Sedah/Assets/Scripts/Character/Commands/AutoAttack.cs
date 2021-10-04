using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sedah;
using Mirror;

[RequireComponent(typeof(CharacterObject))]
[RequireComponent(typeof(EntityStateMachine))]
public abstract class AutoAttack : NetworkBehaviour
{
    private CharacterObject character;
    public GameObject target;
    [SyncVar]
    private double timeLastAttacked;

    [Command]
    protected void CmdSetAutoAttackState(GameObject target)
    {
        EntityStateMachine stateMachine = GetComponent<EntityStateMachine>();
        stateMachine.SetNextState(new AutoAttackingState(stateMachine));
        this.target = target;
    }

    protected void AttemptAutoAttack(GameObject target)
    {
        EntityStateMachine stateMachine = GetComponent<EntityStateMachine>();
        float attackSpeed = character.GetStatValue(StatType.AttackSpeed);
        double timeForNextAttack = double.IsNegativeInfinity(timeLastAttacked) ? double.NegativeInfinity : timeLastAttacked + (1.0 / attackSpeed);
        if (stateMachine.GetState().type == EntityStateType.AutoAttacking)
        {
            // Check if target is in range
            float dist = Vector3.Distance(transform.position, target.transform.position);
            if (dist <= this.character.GetStatValue(StatType.AttackRange))
            {
                // Stop moving if target is in range
                this.GetComponent<PlayerMovement>()?.CmdMove(transform.position);
                if (timeForNextAttack < NetworkTime.time)
                {
                    // If our attack is off cooldown, fire an attack
                    FireAutoAttack(target);
                }
            }
            else
            {
                this.GetComponent<PlayerMovement>()?.CmdMove(target.transform.position);
            }
        }
    }

    protected void FireAutoAttack(GameObject target)
    {
        float damage = character.GetStat(StatType.AttackDamage).Value;
        float attackSpeed = character.GetStatValue(StatType.AttackSpeed);
        DamageInfo dmgInfo = new DamageInfo(damage, gameObject, target.transform.position, DamageType.Physical);
        target.GetComponent<Health>().TakeDamage(dmgInfo);
        timeLastAttacked = NetworkTime.time;
    }

    public override void OnStartServer()
    {
        base.OnStartServer();
        this.character = GetComponent<CharacterObject>();
        this.timeLastAttacked = double.NegativeInfinity;
    }

    // Update is called once per frame
    public void Update()
    {
        if (isServer)
        {
            AttemptAutoAttack(target);
        }
    }
}