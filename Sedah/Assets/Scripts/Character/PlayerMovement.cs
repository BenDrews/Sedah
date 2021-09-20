using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Mirror;

namespace Sedah
{
    public class PlayerMovement : NetworkBehaviour
    {
        NavMeshAgent agent;
        public float movementSpeed = 30;

        private void Start()
        {            
            agent = GetComponent<NavMeshAgent>();
        }


        private void Update()
        {
            if (Input.GetMouseButtonDown(1))
            {
                RaycastHit hit;
                LayerMask layerMask = LayerMask.GetMask("Terrain", "TargetableCharacters");
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                if (Physics.Raycast(ray, out hit, Mathf.Infinity, layerMask))
                {
                    // TODO: Find a better way to represent the terrain layer
                    if (hit.transform.gameObject.layer == 7)
                    {
                        agent.SetDestination(hit.point);
                    }
                }
            }
        }
    }
}
