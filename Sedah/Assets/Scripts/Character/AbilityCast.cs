using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using Sedah;

[RequireComponent(typeof(CharacterObject))]
[RequireComponent(typeof(EntityStateMachine))]
public abstract class AbilityCast : NetworkBehaviour
{
    private CharacterObject character;

    // Ability index
    protected int index = 0;

    // Condition of whether ability needs a point or gameObject target.
    public bool isTarget = false;

    public GameObject target;
    public RaycastHit targetHit;

    Coroutine runCd;

    [Command]
    protected void CmdSetAbilityCastStateTarget(GameObject target, int index)
    {
        EntityStateMachine stateMachine = GetComponent<EntityStateMachine>();
        stateMachine.SetNextState(new CastingState(stateMachine));
        this.target = target;
        this.index = index;
        isTarget = true;
    }

    [Command]
    protected void CmdSetAbilityCastStateHit(RaycastHit hit, int index)
    {
        EntityStateMachine stateMachine = GetComponent<EntityStateMachine>();
        stateMachine.SetNextState(new CastingState(stateMachine));
        this.targetHit = hit;
        this.index = index;
        isTarget = false;
    }

    protected void AttemptAbilityCast(GameObject target)
    {
        EntityStateMachine stateMachine = GetComponent<EntityStateMachine>();

        if (stateMachine.GetState().type == EntityStateType.AbilityCast)
        {
            Ability ability = character.GetAbility(index);
            // Check if target is in range
            float dist = Vector3.Distance(transform.position, target.transform.position);
            if (dist <= ability.GetRange() && !ability.OnCooldown())
            {
                // Stop moving if target is in range
                this.GetComponent<PlayerMovement>()?.CmdMove(transform.position);
                FireAbilityCast(target);
            }
            else
            {
                //TODO" Get the player to move in range of ability cast
                //this.GetComponent<PlayerMovement>()?.CmdMove(target.transform.position);
            }
        }
    }

    protected void AttemptAbilityCast(RaycastHit hit)
    {
        EntityStateMachine stateMachine = GetComponent<EntityStateMachine>();

        if (stateMachine.GetState().type == EntityStateType.AbilityCast)
        {
            Ability ability = character.GetAbility(index);
            // Check if target is in range
            float dist = Vector3.Distance(transform.position, target.transform.position);
            if (dist <= ability.GetRange() && !ability.OnCooldown())
            {
                // Stop moving if target is in range
                this.GetComponent<PlayerMovement>()?.CmdMove(transform.position);
                FireAbilityCast(hit);
            }
            else
            {
                //TODO" Get the player to move in range of ability cast
                //this.GetComponent<PlayerMovement>()?.CmdMove(target.transform.position);
            }
        }
    }

    public IEnumerator RunCooldown(Ability ability, float cd)
    {
        ability.SetCooldown(true);
        yield return new WaitForSeconds(cd);
        ability.SetCooldown(false);
    }

    // Ability affects target.
    protected void FireAbilityCast(GameObject target)
    {
        Ability ability = character.GetAbility(index);
        ability.Activate(character.gameObject, target);
        runCd = StartCoroutine(RunCooldown(ability, ability.GetCooldown() * (1 - character.GetStatValue(StatType.CDR)/100)));
    }

    // Ability affects area/point.
    protected void FireAbilityCast(RaycastHit hit)
    {
        Ability ability = character.GetAbility(index);
        ability.Activate(character.gameObject, hit);
        runCd = StartCoroutine(RunCooldown(ability, ability.GetCooldown() * (1 - character.GetStatValue(StatType.CDR)/100)));
    }

    public override void OnStartServer()
    {
        base.OnStartServer();
        this.character = GetComponent<CharacterObject>();
    }

    // Update is called once per frame
    public void Update()
    {
        if (hasAuthority)
        {
            if (isServer)
            {
                if (isTarget)
                {
                    AttemptAbilityCast(target);
                }
                else
                {
                    AttemptAbilityCast(targetHit);
                }
            }
        }
    }
}