using UnityEngine;

[CreateAssetMenu(fileName = "Ability", menuName = "ScriptableObjects/Ability")]
public class AbilityDataObject : ScriptableObject
{
    [SerializeField] private int id;
    [SerializeField] private Sprite sprite; // Drag & drop the sprite. It does not need to be in the `Resources` folder
    [SerializeField, Range(0, 999)] private int cost = 0;
    [SerializeField, Range(0, 999)] private int damage = 0;
    [SerializeField] private string target  = "";
    [SerializeField] private string effect = "";

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

    public int Cost
    {
        get { return cost; }
        private set { cost = value; }
    }

    public int Damage
    {
        get { return damage; }
        private set { damage = value; }
    }

    public string Target
    {
        get { return target; }
        private set { target = value; }
    }

    public string Effect
    {
        get { return effect; }
        private set { effect = value; }
    }

    public AbilityDataObject(int id, Sprite sprite, int cost, int damage, string target, string effect)
    {
        ID = id;
        Sprite = sprite;
        Cost = cost;
        Damage = damage;
        Target = target;
        Effect = effect;
    }
}