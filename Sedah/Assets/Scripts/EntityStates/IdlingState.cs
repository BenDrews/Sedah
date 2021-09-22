using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdlingState : EntityState
{

    public IdlingState(EntityStateMachine entityStateMachine) : base(entityStateMachine)
    {
        type = EntityStateType.Idling;
    }
}
