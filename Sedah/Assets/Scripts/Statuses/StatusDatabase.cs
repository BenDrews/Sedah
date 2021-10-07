using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

[CreateAssetMenu(fileName = "New Status Database", menuName = "ScriptableObjects/StatusDatabase")]

public class StatusDatabase : ScriptableObject
{
    public StatusData[] StatusData;

    public void OnValidate()
    {
        for (int i = 0; i < StatusData.Length; i++)
        {
            StatusData[i].id = i;
        }
    }

    public StatusData GetStatus(int i)
    {
        return StatusData[i];
    }
}