using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class InventorySlot
{
    [SerializeField] private Sprite sprite; // Drag & drop the sprite. It does not need to be in the `Resources` folder
    public Item item;
    public int amount;
    public int ID = -1;
    public InventorySlot()
    {
        ID = -1;
        item = null;
        amount = 0;
    }
    public InventorySlot(int _id, Item _item, int _amount)
    {
        ID = _id;
        item = _item;
        amount = _amount;
    }
    public void UpdateSlot(int _id, Item _item, int _amount)
    {
        ID = _id;
        item = _item;
        amount = _amount;
    }
    public void AddAmount(int value)
    {
        amount += value;
    }
    public Sprite Sprite
    {
        get { return sprite; }
        private set { sprite = value; }
    }

}