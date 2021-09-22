using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoAttackingState : EntityState
{
    public AutoAttackingState(EntityStateMachine entityStateMachine) : base(entityStateMachine)
    {
        type = EntityStateType.AutoAttacking;
    }
}
