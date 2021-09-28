using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobAbilityCast : ServerAbilityCast
{
    // Start is called before the first frame update
    public void Cast(GameObject obj, int i)
    {
        SetAbilityCastStateTarget(obj, i);
    }

    public void Cast(Vector3 point, int i)
    {
        SetAbilityCastStatePoint(point, i);
    }

}
