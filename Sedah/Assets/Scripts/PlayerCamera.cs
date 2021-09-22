using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using Sedah;

public class PlayerCamera : NetworkBehaviour
{
    [SerializeField]
    private GameObject localPlayerCameraTargetPrefab;
    // Start is called before the first frame update
    public override void OnStartClient()
    {

        base.OnStartClient();
        GameObject playerCamTarget = GameObject.Instantiate(localPlayerCameraTargetPrefab);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
