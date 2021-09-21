using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

[System.Serializable]
public abstract class Ability: NetworkBehaviour
{
    [SerializeField] protected int ID;
    [SerializeField] protected Sprite Sprite; // Drag & drop the sprite. It does not need to be in the `Resources` folder
    [SerializeField, Range(0, 999)] protected int Cost = 0;
    [SerializeField, Range(0, 999)] protected int Damage = 0;
    [SerializeField] protected string Target = "";
    [SerializeField] protected string Effect = "";
    [SerializeField] protected int CooldownTime = 0;
    [SerializeField] protected bool OnCooldown = false;
    [SerializeField] protected Coroutine RunCd;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public IEnumerator RunCooldown()
    {
        OnCooldown = true;
        yield return new WaitForSeconds(CooldownTime);
        OnCooldown = false;
    }

    public abstract void Activate(GameObject gameObject1 = null, GameObject gameObject2 = null);


    public bool IsOnCooldown()
    {
        return OnCooldown;
    }

    public int GetCost()
    {
        return Cost;
    }

    public void SetCooldown(bool cd)
    {
        OnCooldown = cd;
    }

}
