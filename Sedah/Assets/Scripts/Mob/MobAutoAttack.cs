using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterObject))]
[RequireComponent(typeof(EntityStateMachine))]
public class MobAutoAttack : ServerAutoAttack
{
    // Update is called once per frame
    public void Attack(GameObject obj)
    {
        EntityStateMachine stateMachine = GetComponent<EntityStateMachine>();
        if (Input.GetMouseButtonDown(1))
        {
            if (obj.GetComponent<Health>() != null)
            {
                SetAutoAttackState(obj);
            }
        }
        base.Update();
    }
}