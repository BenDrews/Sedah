using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Mirror;

namespace Sedah
{
    public class PlayerMovement : Movement
    {
        public new void Update()
        {
            base.Update();
            if (hasAuthority)
            {
                if (Input.GetMouseButtonDown(1))
                {
                    RaycastHit hit;
                    LayerMask layerMask = LayerMask.GetMask("Terrain", "TargetableCharacters");
                    Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                    if (Physics.Raycast(ray, out hit, Mathf.Infinity, layerMask))
                    {
                        if (hit.transform.gameObject.layer == LayerMask.NameToLayer("Terrain"))
                        {
                            CmdSetMoveState(hit.point);
                        }
                    }
                }
            }
        }
    }
}
