using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.AI;
using Mirror;

namespace Sedah
{
    [RequireComponent(typeof(NavMeshAgent))]
    [RequireComponent(typeof(CharacterObject))]
    [RequireComponent(typeof(EntityStateMachine))]
    public class Movement : NetworkBehaviour
    {
        private NavMeshAgent agent;
        private EntityStateMachine stateMachine;
        private float movementSpeed;

        public override void OnStartServer()
        {
            agent = GetComponent<NavMeshAgent>();
            stateMachine = GetComponent<EntityStateMachine>();
            movementSpeed = GetComponent<CharacterObject>().GetStatValue(StatType.MoveSpeed);
            base.OnStartServer();
        }

        [Command]
        public void CmdMove(Vector3 pos)
        {
            agent.SetDestination(pos);
        }

        [Command]
        public void CmdSetMoveState(Vector3 pos)
        {
            EntityStateMachine stateMachine = GetComponent<EntityStateMachine>();
            stateMachine.SetNextState(new MovingState(stateMachine));
            agent.SetDestination(pos);
        }

        public void Update()
        {
            if (isServer)
            {
                if (stateMachine.GetState().type == EntityStateType.Moving)
                {
                    if (agent.isStopped)
                    {
                        stateMachine.SetNextState(new IdlingState(stateMachine));
                    }
                    else if (agent.speed != movementSpeed)
                    {
                        agent.speed = movementSpeed;
                    }
                }
            }
        }
    }
}