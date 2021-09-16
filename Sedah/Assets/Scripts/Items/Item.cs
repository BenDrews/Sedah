using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemType
{
    Potion
}
public class Item : ScriptableObject
{
    [SerializeField] private int id;
    [SerializeField] private Sprite sprite; // Drag & drop the sprite. It does not need to be in the `Resources` folder
    public ItemType type;
    [TextArea(15, 20)]
    public string description;
    public Sprite Sprite
    {
        get { return sprite; }
        private set { sprite = value; }
    }
    public int ID
    {
        get { return id; }
        private set { id = value; }
    }


}