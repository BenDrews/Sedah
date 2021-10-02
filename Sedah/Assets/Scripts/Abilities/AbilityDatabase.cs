using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

[CreateAssetMenu(fileName = "New Ability Database", menuName = "ScriptableObjects/AbilityDatabase")]

public class AbilityDatabase : ScriptableObject, ISerializationCallbackReceiver
{
    public Ability[] Abilities;
    public Dictionary<int, Ability> getAbility = new Dictionary<int, Ability>();

    public void OnAfterDeserialize()
    {
        getAbility = new Dictionary<int, Ability>();
        for (int i = 0; i < Abilities.Length; i++)
        {
            getAbility.Add(i, Abilities[i]);
        }
    }

    public Ability GetAbility(int id)
    {
        return getAbility[id];
    }

    public void OnBeforeSerialize()
    {
        getAbility = new Dictionary<int, Ability>();
    }
}