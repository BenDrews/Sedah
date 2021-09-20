using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Character", menuName = "ScriptableObjects/Character")]
public class Character : ScriptableObject
{

    [SerializeField] private int id;
    [SerializeField] private string name;
    [SerializeField] private Sprite sprite; // Drag & drop the sprite. It does not need to be in the `Resources` folder
    [SerializeField] private float health = 0;
    [SerializeField] private float healthPerLvl = 0;
    [SerializeField] private float mana = 0;
    [SerializeField] private float manaPerLvl = 0;
    [SerializeField] private float attackDamage = 0;
    [SerializeField] private float adPerLvl = 0;
    [SerializeField] private float attackSpeed = 0;
    [SerializeField] private float asPerLvl = 0;
    [SerializeField] private float attackRange = 0;
    [SerializeField] private float moveSpeed = 0;
    [SerializeField] private float armor = 0;
    [SerializeField] private float armorPerLvl = 0;
    [SerializeField] private float magicRes = 0;
    [SerializeField] private float mrPerLvl = 0;
    [SerializeField] private List<int> abilities = new List<int>();


    // Start is called before the first frame update
    void Start()
    {

    }
    public int ID
    {
        get { return id; }
        private set { id = value; }
    }

    public string Name
    {
        get { return name; }
        private set { name = value; }
    }

    public Sprite Sprite
    {
        get { return sprite; }
        private set { sprite = value; }
    }

    public float Health
    {
        get { return health; }
        private set { health = value; }

    }

    public float HealthPerLvl
    {
        get { return healthPerLvl; }
        private set { healthPerLvl = value; }
    }

    public float Mana
    {
        get { return mana; }
        private set { mana = value; }
    }

    public float ManaPerLvl
    {
        get { return manaPerLvl; }
        private set { manaPerLvl = value; }
    }

    public float AttackDamage
    {
        get { return attackDamage; }
        private set { attackDamage = value; }
    }

    public float ADPerLvl
    {
        get { return adPerLvl; }
        private set { adPerLvl = value; }
    }

    public float AttackSpeed
    {
        get { return attackSpeed; }
        private set { attackSpeed = value; }
    }

    public float ASPerLvl
    {
        get { return asPerLvl; }
        private set { asPerLvl = value; }
    }

    public float AttackRange
    {
        get { return attackRange; }
        private set { AttackRange = value; }
    }

    public float MoveSpeed
    {
        get { return moveSpeed; }
        private set { moveSpeed = value; }
    }

    public float Armor
    {
        get { return armor; }
        private set { armor = value; }
    }

    public float ArmorPerLvl
    {
        get { return armorPerLvl; }
        private set { armorPerLvl = value; }
    }
    public float MagicRes
    {
        get { return magicRes; }
        private set { magicRes = value; }
    }

    public float MRPerLvl
    {
        get { return mrPerLvl; }
        private set { mrPerLvl = value; }
    }

    public List<int> GetAbilities()
    {
        return abilities;
    }

}
