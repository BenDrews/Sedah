using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EntityState
{
    protected EntityStateMachine EntityStateMachine;

    protected EntityState (EntityStateMachine entityStateMachine)
    {
        EntityStateMachine = entityStateMachine;
    }
    // Start is called before the first frame update
    public virtual IEnumerator Start()
    {
        yield break;
    }
    public virtual IEnumerator Idle()
    {
        yield break;

    }
    public virtual IEnumerator Walking()
    {
        yield break;
    }

    public virtual IEnumerator Running()
    {
        yield break;
    }

    public virtual IEnumerator Attack()
    {
        yield break;
    }

    public virtual IEnumerator Heal()
    {
        yield break;
    }

    public virtual IEnumerator Cast()
    {
        yield break;
    }

    public virtual IEnumerator Death()
    {
        yield break;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
