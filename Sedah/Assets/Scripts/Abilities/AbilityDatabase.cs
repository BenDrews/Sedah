using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Ability Database", menuName = "ScriptableObjects/AbilityDatabase")]

public class AbilityDatabase : ScriptableObject, ISerializationCallbackReceiver
{
    public GameObject[] Abilities;
    public Dictionary<int, GameObject> getAbility = new Dictionary<int, GameObject>();

    public void OnAfterDeserialize()
    {
        getAbility = new Dictionary<int, GameObject>();
        for (int i = 0; i < Abilities.Length; i++)
        {
            getAbility.Add(i, Abilities[i]);
        }
    }

    public GameObject GetAbility(int id)
    {
        return getAbility[id];
    }

    public void OnBeforeSerialize()
    {
        getAbility = new Dictionary<int, GameObject>();
    }
}