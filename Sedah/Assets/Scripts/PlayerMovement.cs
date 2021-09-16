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
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                if (Physics.Raycast(ray, out hit, Mathf.Infinity))
                {
                    agent.SetDestination(hit.point);
                }
            }
        }
    }
}
