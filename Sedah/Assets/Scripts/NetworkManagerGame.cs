using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

namespace Sedah
{
    // Custom NetworkManager that simply assigns the correct racket positions when
    // spawning players. The built in RoundRobin spawn method wouldn't work after
    // someone reconnects (both players would be on the same side).
    [AddComponentMenu("")]
    public class NetworkManagerGame : NetworkManager
    {
        public Transform playerSpawn;

        public override void OnServerAddPlayer(NetworkConnection conn)
        {
            // add player at correct spawn position

            GameObject player = Instantiate(playerPrefab, playerSpawn.position, playerSpawn.rotation);
            NetworkServer.AddPlayerForConnection(conn, player);
        }

        public override void OnServerDisconnect(NetworkConnection conn)
        {
            // call base functionality (actually destroys the player)
            base.OnServerDisconnect(conn);
        }
    }
}
