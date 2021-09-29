using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public enum TargetingType
{
    Self,
    Enemy,
    Ally,
    Point
}

[System.Serializable]
public abstract class Ability : NetworkBehaviour
{
    [SerializeField] protected int id;
    [SerializeField] protected Sprite sprite; // Drag & drop the sprite. It does not need to be in the `Resources` folder
    [SerializeField, Range(0, 999)] protected int cost = 0;
    [SerializeField, Range(0, 999)] protected int damage = 0;
    [SerializeField] protected float range = 0;
    [SerializeField] protected TargetingType target;
    [SerializeField] protected int cooldownTime = 0;
    [SerializeField, HideInInspector] protected bool onCooldown = false;

    public int GetCost()
    {
        return cost;
    }

    public bool OnCooldown()
    {
        return onCooldown;
    }

    public void SetCooldown(bool cd)
    {
        onCooldown = cd;
    }

    public int GetCooldown()
    {
        return cooldownTime;
    }

    public float GetRange()
    {
        return range;
    }

    public TargetingType GetTarget()
    {
        return target;
    }

    
    public virtual void Activate(GameObject characterObject, GameObject gameObject2)
    {

    }

    public virtual void Activate(GameObject characterObject, Vector3 point)
    {

    }
}
