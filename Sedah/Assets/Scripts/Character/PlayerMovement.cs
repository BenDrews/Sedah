using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Mirror;

namespace Sedah
{
    [RequireComponent(typeof(EntityStateMachine))]
    public class PlayerMovement : NetworkBehaviour
    {
        private NavMeshAgent agent;

        public float movementSpeed = 30;
        private void Start()
        {
            //// This script should only exist on the local player
            //if (NetworkClient.localPlayer.netId != base.netId)
            //{
            //    Destroy(this);
            //}
            agent = GetComponent<NavMeshAgent>();
        }

        [Command]
        public void CmdMove(Vector3 pos)
        {                       
            agent.SetDestination(pos);
            Debug.Log(pos);
            EntityStateMachine stateMachine = GetComponent<EntityStateMachine>();
            stateMachine.SetNextState(new MovingState(stateMachine));
        }

        public void Update()
        {
            if (hasAuthority)
            {
                if (Input.GetMouseButtonDown(1))
                {
                    RaycastHit hit;
                    LayerMask layerMask = LayerMask.GetMask("Terrain", "TargetableCharacters");
                    Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                    Debug.Log(ray);
                    if (Physics.Raycast(ray, out hit, Mathf.Infinity, layerMask))
                    {
                        Debug.Log(ray + "YES");
                        // TODO: Find a better way to represent the terrain layer
                        if (hit.transform.gameObject.layer == LayerMask.NameToLayer("Terrain"))
                        {
                            Debug.Log(ray + "YE3S");
                            CmdMove(hit.point);
                        }
                    }
                }
            }
        }
    }
}
