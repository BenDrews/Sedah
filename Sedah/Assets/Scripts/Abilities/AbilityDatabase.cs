using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

[CreateAssetMenu(fileName = "New Ability Database", menuName = "ScriptableObjects/AbilityDatabase")]

public class AbilityDatabase : ScriptableObject
{
    public AbilityData[] AbilityData;

    public void OnValidate()
    {
        for (int i = 0; i < AbilityData.Length; i++)
        {
            AbilityData[i].id = i;
        }
    }

    public AbilityData GetAbility(int id)
    {
        return AbilityData[id];
    }
}