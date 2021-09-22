using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EntityState
{
    protected EntityStateMachine EntityStateMachine;
    public EntityStateType type;

    protected EntityState (EntityStateMachine entityStateMachine)
    {
        EntityStateMachine = entityStateMachine;
    }

    public virtual void OnEnter()
    {
        return;
    }

    public virtual void OnExit()
    {
        return;
    }
}
