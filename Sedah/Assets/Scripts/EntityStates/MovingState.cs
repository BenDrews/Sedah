using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingState : EntityState
{

    public MovingState(EntityStateMachine entityStateMachine) : base(entityStateMachine)
    {
        type = EntityStateType.Moving;
    }
}
