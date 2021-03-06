using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item Database", menuName = "ScriptableObjects/ItemDatabase")]
public class ItemDatabase : ScriptableObject, ISerializationCallbackReceiver
{
    public Item[] Items;
    public Dictionary<Item, int> GetId = new Dictionary<Item, int>();
    public Dictionary<int, Item> GetItem = new Dictionary<int, Item>();

    public void OnAfterDeserialize()
    {
        GetId = new Dictionary<Item, int>();
        GetItem = new Dictionary<int, Item>();
        for (int i = 0; i < Items.Length; i++)
        {
            GetId.Add(Items[i], i);
            GetItem.Add(i, Items[i]);
        }
    }

    public void OnBeforeSerialize()
    {
        GetItem = new Dictionary<int, Item>();
    }
}
