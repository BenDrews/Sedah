using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using UnityEngine.SceneManagement;

namespace Sedah
{
    // Custom NetworkManager that simply assigns the correct racket positions when
    // spawning players. The built in RoundRobin spawn method wouldn't work after
    // someone reconnects (both players would be on the same side).
    public class NetworkManagerGame : NetworkManager
    {
        public override void OnServerDisconnect(NetworkConnection conn)
        {
            // call base functionality (actually destroys the player)
            base.OnServerDisconnect(conn);
        }
    }
}
