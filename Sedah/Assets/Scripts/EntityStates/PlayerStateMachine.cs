using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateMachine : EntityStateMachine
{

    // Start is called before the first frame update
    void Start()
    {
        SetState(new Begin(this));
    }

    public void Attack()
    {
        StartCoroutine(State.Attack());
    }

    public void Walk()
    {
        StartCoroutine(State.Walking());
    }

    public void Stop()
    {
        StartCoroutine(State.Idle());
    }

    public void Cast()
    {
        StartCoroutine(State.Cast());
    }
}
