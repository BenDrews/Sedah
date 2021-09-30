using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

[RequireComponent(typeof(NetworkTransform))]
public abstract class Aoe : NetworkBehaviour
{
    protected double timeCreated;
    [HideInInspector]
    public CharacterObject owner;
    [HideInInspector]
    public AbilityEffect[] abilityEffects;

    public AbilityEffectData[] abilityEffectsData;

    protected abstract bool ShouldFireAoe();
    protected abstract bool ShouldDestroy();
    protected abstract Collider[] HitColliders();
    protected bool CanHit(CharacterObject target)
    {
        return owner.team != target.team;
    }

    protected void HitTarget(CharacterObject target)
    {
        for (int i = 0; i < abilityEffects.Length; i++)
        {
            abilityEffects[i].Invoke(owner, target);
        }
    }

    protected void Fire()
    {
        Collider[] hitColliders = HitColliders();
        foreach (var hitCollider in hitColliders)
        {
            CharacterObject target = hitCollider.gameObject.GetComponent<CharacterObject>();
            if (CanHit(target))
            {
                HitTarget(target);
            }
        }
    }

    public void Start()
    {
        timeCreated = NetworkTime.time;
        abilityEffects = new AbilityEffect[abilityEffectsData.Length];
        for (int i = 0; i < abilityEffectsData.Length; i++)
        {
            AbilityEffectData data = abilityEffectsData[i];
            String typeString = data.type;
            Type type = Type.GetType(typeString);
            object[] args = { data };
            abilityEffects[i] = (AbilityEffect)Activator.CreateInstance(type, args);
        }
    }

    public void Update()
    {
        if (isServer) { 
            if (ShouldFireAoe())
            {
                Fire();
            }
            if (ShouldDestroy())
            {
                foreach (Transform child in transform)
                {
                    Destroy(child);
                }
                Destroy(this);
            }
        }
    }
}
