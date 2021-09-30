using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class PillarOfFire : Ability
{
    [SerializeField] protected GameObject aoePrefab;
    public float delay;
    public float radius;
    public override void Activate(GameObject attacker, Vector3 target)
    {        
        GameObject aoeObj = Instantiate<GameObject>(aoePrefab, target, Quaternion.identity);
        DelayedCircleAoe aoe = aoeObj.GetComponent<DelayedCircleAoe>();
        aoe.owner = attacker.GetComponent<CharacterObject>();
    }
}
