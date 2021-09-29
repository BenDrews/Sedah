using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterTargetedProjectile : Projectile
{
    [HideInInspector]
    public CharacterObject target;


    protected override Vector3 GetTargetDirection() {
        return (target.transform.position - transform.position).normalized;
    }

    protected override bool ShouldDestroy()
    {
        return targetsHit.Count > 0;
    }


    protected new bool CanHit(CharacterObject target)
    {
        return target == this.target;
    }

    protected new void ValidateProjectile()
    {
        Debug.Assert(target != null, "Projectile [" + gameObject.name + "] needs to have a target before firing");
        base.ValidateProjectile();
    }
}
