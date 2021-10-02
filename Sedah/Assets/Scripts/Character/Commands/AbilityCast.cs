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
    private Animator animator;
    // Condition of whether ability needs a point or gameObject target.
    public bool isTarget = false;
    public CharacterObject target;
    public Vector3 point;

    Coroutine runCd;

    [Command]
    protected void CmdSetAbilityCastStateTarget(GameObject target, int index)
    {
        EntityStateMachine stateMachine = GetComponent<EntityStateMachine>();
        stateMachine.SetNextState(new CastingState(stateMachine));
        this.target = target.GetComponent<CharacterObject>();
        this.index = index;
        isTarget = true;
        animator.SetBool("Casting", true);
    }

    [Command]
    protected void CmdSetAbilityCastStateHit(Vector3 point, int index)
    {
        EntityStateMachine stateMachine = GetComponent<EntityStateMachine>();
        stateMachine.SetNextState(new CastingState(stateMachine));
        this.point = point;
        this.index = index;
        isTarget = false;
        animator.SetBool("Casting", true);
    }

    protected void AttemptAbilityCast(CharacterObject target)
    {
        EntityStateMachine stateMachine = GetComponent<EntityStateMachine>();

        if (stateMachine.GetState().type == EntityStateType.AbilityCast)
        {
            AbilityTemplate ability = character.GetAbility(index);
            // Check if target is in range
            float dist = Vector3.Distance(transform.position, target.transform.position);
            if (dist <= ability.GetRange() && !ability.OnCooldown())
            {
                // Stop moving if target is in range
                //this.GetComponent<PlayerMovement>()?.CmdMove(transform.position);
                FireAbilityCast(target);
            }
            else
            {
                animator.SetBool("Casting", false);
                //TODO" Get the player to move in range of ability cast
                //this.GetComponent<PlayerMovement>()?.CmdMove(target.transform.position);
            }
        }
    }

    protected void AttemptAbilityCast(Vector3 point)
    {
        EntityStateMachine stateMachine = GetComponent<EntityStateMachine>();

        if (stateMachine.GetState().type == EntityStateType.AbilityCast)
        {
            AbilityTemplate ability = character.GetAbility(index);
            // Check if target is in range
            float dist = Vector3.Distance(transform.position, point);
            if (dist <= ability.GetRange() && !ability.OnCooldown())
            {
                // Stop moving if target is in range
                //this.GetComponent<PlayerMovement>()?.CmdMove(transform.position);
                FireAbilityCast(point);
            }
            else
            {
                animator.SetBool("Casting", false);
                //TODO" Get the player to move in range of ability cast
                //this.GetComponent<MobMovement>()?.Move(target.transform.position);
            }
        }
    }

    public IEnumerator RunCooldown(AbilityTemplate ability, float cd)
    {
        ability.SetCooldown(true);
        yield return new WaitForSeconds(cd);
        ability.SetCooldown(false);
    }

    // Ability affects target.
    protected void FireAbilityCast(CharacterObject target)
    {
        AbilityTemplate ability = character.GetAbility(index);
        ability.Activate(character, target);
        runCd = StartCoroutine(RunCooldown(ability, ability.GetCooldown() * (1 - character.GetStatValue(StatType.CDR)/100)));
    }

    // Ability affects area/point.
    protected void FireAbilityCast(Vector3 point)
    {
        AbilityTemplate ability = character.GetAbility(index);
        ability.Activate(character, point);
        runCd = StartCoroutine(RunCooldown(ability, ability.GetCooldown() * (1 - character.GetStatValue(StatType.CDR)/100)));
    }

    public override void OnStartServer()
    {
        base.OnStartServer();
        this.character = GetComponent<CharacterObject>();
        animator = GetComponent<Animator>();
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
                    AttemptAbilityCast(point);
                }
            }
        }
    }
}