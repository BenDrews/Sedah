using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.AI;
using Mirror;

namespace Sedah
{
    [RequireComponent(typeof(NavMeshAgent))]
    [RequireComponent(typeof(CharacterObject))]
    public class ServerMovement : NetworkBehaviour
    {
        private NavMeshAgent agent;
        private EntityStateMachine stateMachine;
        private Animator animator;
        private Vector3 destination;
        private float movementSpeed;
        private float rotationSpeed = 30;

        public override void OnStartServer()
        {
            agent = GetComponent<NavMeshAgent>();
            stateMachine = GetComponent<EntityStateMachine>();
            animator = GetComponent<Animator>();
            movementSpeed = GetComponent<CharacterObject>().GetStatValue(StatType.MoveSpeed);
            base.OnStartServer();
        }

        public void Move(Vector3 pos)
        {
            destination = pos;
            agent.SetDestination(destination);
            agent.isStopped = false;
            animator.SetBool("Moving", true);
        }

        public void SetMoveState(Vector3 pos)
        {
            destination = pos;
            stateMachine.SetNextState(new MovingState(stateMachine));
            agent.SetDestination(destination);
            agent.isStopped = false;
            animator.SetBool("Moving", true);
        }

        public void Update()
        {
            if (isServer)
            {
                if (stateMachine.GetState().type == EntityStateType.Moving)
                {
                    Vector3 currentPos = transform.position;
                    currentPos.y = 0;
                    if (Vector3.Distance(currentPos, destination) < 0.01f)
                    {
                        agent.isStopped = true;
                        stateMachine.SetNextState(new IdlingState(stateMachine));
                        animator.SetBool("Moving", false);
                    }
                    agent.speed = movementSpeed;
                    Vector3 dir = destination - transform.position;
                    dir.y = 0;
                    Quaternion rot = Quaternion.LookRotation(dir);
                    transform.rotation = Quaternion.Lerp(transform.rotation, rot, rotationSpeed * Time.deltaTime);
                }
            }
        }
    }
}