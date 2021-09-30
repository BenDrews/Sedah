using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class DelayedCircleAoe : Aoe
{
    private bool hasFired = false;

    public float delay;
    public float duration;
    public float radius;

    protected override Collider[] HitColliders()
    {
        return Physics.OverlapSphere(transform.position, radius);
    }

    protected override bool ShouldFireAoe()
    {
        return NetworkTime.time > timeCreated + duration && !hasFired;
    }

    protected override bool ShouldDestroy()
    {
        return NetworkTime.time > timeCreated + duration;
    }

    protected new void Fire()
    {
        Debug.Log("Firing AOE");
        base.Fire();
        hasFired = true;
    }

}
