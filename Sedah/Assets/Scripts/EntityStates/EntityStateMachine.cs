using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class EntityStateMachine : NetworkBehaviour
{
    protected EntityState state;
    protected EntityState nextState;

    public void Awake()
    {
        state = new IdlingState(this);
    }

    public void SetNextState(EntityState state)
    {
        nextState = state;
    }

    public EntityState GetState()
    {
        return state;
    }

    public void Update()
    {
        if (nextState != null)
        {
            state.OnExit();
            state = nextState;
            nextState.OnEnter();
        }
    }
}
