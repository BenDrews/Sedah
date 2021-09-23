using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using UnityEngine.SceneManagement;
using UnityEngine.AI;

namespace Sedah
{
    // Custom NetworkManager that simply assigns the correct racket positions when
    // spawning players. The built in RoundRobin spawn method wouldn't work after
    // someone reconnects (both players would be on the same side).
    public class NetworkManagerGame : NetworkManager
    {
        [SerializeField] GameObject camera;
        public override void OnServerSceneChanged(string aceneName)
        {
            GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
            for (int i = 0; i < players.Length; i++)
            {
                players[i].GetComponent<EntityStateMachine>().enabled = true;
                players[i].GetComponent<NavMeshAgent>().enabled = true;
                players[i].GetComponent<PlayerAbility>().enabled = true;
                players[i].GetComponent<MeshRenderer>().enabled = true;
                players[i].GetComponent<CapsuleCollider>().enabled = true;
                players[i].GetComponent<PlayerMovement>().enabled = true;
                players[i].GetComponent<Health>().enabled = true;
                players[i].GetComponent<CharacterObject>().enabled = true;
                players[i].GetComponent<Player>().enabled = true;
                players[i].GetComponent<PlayerAutoAttack>().enabled = true;

            }
        }

        public override void OnClientSceneChanged(NetworkConnection conn)
        {
            base.OnClientSceneChanged(conn);
            Scene scene = SceneManager.GetActiveScene();
            //GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
            //for (int i = 0; i < players.Length; i++)
            //{
            //    this.GetComponent<EntityStateMachine>().enabled = true;
            //    this.GetComponent<NavMeshAgent>().enabled = true;
            //    this.GetComponent<PlayerAbility>().enabled = true;
            //    this.GetComponent<MeshRenderer>().enabled = true;
            //    this.GetComponent<CapsuleCollider>().enabled = true;
            //    this.GetComponent<PlayerMovement>().enabled = true;
            //    this.GetComponent<Health>().enabled = true;
            //    this.GetComponent<CharacterObject>().enabled = true;
            //    this.GetComponent<Player>().enabled = true;
            //    //this.GetComponent<PlayerCamera>().enabled = true;
            //    this.GetComponent<PlayerAutoAttack>().enabled = true;

            //}
            if (scene.name == "BenScene")
            {
                Debug.Log("GYA");

                GameObject playerCamTarget = Camera.Instantiate(camera);
            }
        }
        public override void OnServerDisconnect(NetworkConnection conn)
        {
            // call base functionality (actually destroys the player)
            base.OnServerDisconnect(conn);
        }
    }
}
