using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
public class spawnPlayerChars : NetworkBehaviour
{
    public GameObject playerSpawn;
    // Start is called before the first frame update
    public override void OnStartServer()
    {
        GameObject pSpawn = Instantiate(playerSpawn);
        NetworkServer.Spawn(pSpawn, connectionToClient);
    }
}
