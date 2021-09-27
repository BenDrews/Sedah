using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class PlayerChooseLevel : NetworkBehaviour
{

    public void ChangeToLevel(int level)
    {
        CmdChangeScene(level);
    }

    [Command]

    void CmdChangeScene(int level)
    {
        if (level == 1)
        {
            NetworkManager.singleton.ServerChangeScene("BenScene");
        }
        else if (level == 2)
        {
            NetworkManager.singleton.ServerChangeScene("BenScene2");
        }
        else if (level == 3)
        {
            NetworkManager.singleton.ServerChangeScene("BenScene3");
        }
    }

}
