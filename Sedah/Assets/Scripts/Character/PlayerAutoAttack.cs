using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sedah;
using Mirror;

[RequireComponent(typeof(CharacterObject))]
[RequireComponent(typeof(EntityStateMachine))]
public class PlayerAutoAttack : AutoAttack
{
    // Update is called once per frame
    public new void Update()
    {
        if (hasAuthority)
        {
            EntityStateMachine stateMachine = GetComponent<EntityStateMachine>();
            if (Input.GetMouseButtonDown(1))
            {
                RaycastHit hit;
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                LayerMask layerMask = LayerMask.GetMask("TargetableCharacters");
                if (Physics.Raycast(ray, out hit, Mathf.Infinity, layerMask))
                {
                    GameObject obj = hit.transform.gameObject;
                    if (obj.GetComponent<Health>() != null)
                    {
                        CmdSetAutoAttackState(obj);
                    }
                }
            }
            base.Update();
        }
    }
}
