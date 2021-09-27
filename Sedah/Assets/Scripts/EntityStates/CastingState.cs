using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CastingState : EntityState
{

    public CastingState(EntityStateMachine entityStateMachine) : base(entityStateMachine)
    {
        type = EntityStateType.AbilityCast;
    }
}
