using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Mob", menuName = "ScriptableObjects/Mob")]
public class Mob : ScriptableObject
{

    [SerializeField] private int id;
    [SerializeField] private Sprite sprite; // Drag & drop the sprite. It does not need to be in the `Resources` folder
    [SerializeField] public Dictionary<string, Stat> characterStats = new Dictionary<string, Stat>();
    [SerializeField] public Dictionary<string, Status> statusEffects = new Dictionary<string, Status>();

    //public InventoryObject inventory;

    // Start is called before the first frame update
    void Start()
    {

    }
    public int ID
    {
        get { return id; }
        private set { id = value; }
    }

    public Sprite Sprite
    {
        get { return sprite; }
        private set { sprite = value; }
    }

    public Mob(int id, Sprite sprite)
    {
        ID = id;
        Sprite = sprite;
    }

}
