using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class LevelManager : MonoBehaviour
{

    public void VoteLevel(int level)
    {
        Player.localPlayer.gameObject.GetComponent<PlayerChooseLevel>().ChangeToLevel(level);
    }
}
