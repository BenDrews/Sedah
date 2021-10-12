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
                ValidateComponents(players[i]);
            }
        }

        public override void OnClientSceneChanged(NetworkConnection conn)
        {
            base.OnClientSceneChanged(conn);
            Scene scene = SceneManager.GetActiveScene();
            if (scene.name != "Lobby" && scene.name != "Offline")
            {
                GameObject playerCamTarget = Camera.Instantiate(camera);
                ValidateComponents(Player.localPlayer.gameObject);
                GameObject uiScreen = GameObject.FindGameObjectWithTag("UIScreen");
                if (uiScreen != null)
                {
                    Player.localPlayer.GetComponent<PlayerToggleScreen>().SetMenu(GameObject.FindGameObjectWithTag("FloorSelector"));
                    uiScreen.GetComponentInChildren<HealthDisplayHUD>().target = Player.localPlayer.gameObject;
                    uiScreen.GetComponent<Canvas>().worldCamera = playerCamTarget.GetComponent<Camera>();
                }
            }
        }
        public override void OnServerDisconnect(NetworkConnection conn)
        {
            // call base functionality (actually destroys the player)
            base.OnServerDisconnect(conn);
        }

        private void ValidateComponents(GameObject player)
        {
            player.GetComponent<EntityStateMachine>().enabled = true;
            player.GetComponent<NavMeshAgent>().enabled = true;
            player.GetComponent<PlayerAbility>().enabled = true;
            player.GetComponent<MeshRenderer>().enabled = true;
            player.GetComponent<CapsuleCollider>().enabled = true;
            player.GetComponent<PlayerMovement>().enabled = true;
            player.GetComponent<Health>().enabled = true;
            player.GetComponent<CharacterObject>().enabled = true;
            player.GetComponent<Player>().enabled = true;
            player.GetComponent<PlayerAutoAttack>().enabled = true;
            player.GetComponent<PlayerToggleScreen>().enabled = true;
        }
    }
}
