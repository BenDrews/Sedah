using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public abstract class EntityStateMachine: NetworkBehaviour
{
    protected EntityState State;

    public void SetState(EntityState state)
    {
        State = state;
        StartCoroutine(State.Start());
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
