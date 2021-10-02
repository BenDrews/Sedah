using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

[RequireComponent(typeof(NetworkTransform))]
public abstract class Projectile : NetworkBehaviour
{
    [HideInInspector]
    public CharacterObject owner;
    [HideInInspector]
    public AbilityEffect[] abilityEffects;
    public float speed;

    protected bool hasFired;
    protected double timeFired;
    protected List<CharacterObject> targetsHit = new List<CharacterObject>();


    protected abstract Vector3 GetTargetDirection();
    protected abstract bool ShouldDestroy();

    public void Fire()
    {
        if (Debug.isDebugBuild)
        {
            ValidateProjectile();
        }
        hasFired = true;
        timeFired = NetworkTime.time;
    }

    protected void ValidateProjectile()
    {
        Debug.Assert(owner != null, "Projectile [" + gameObject.name + "] needs to be fired by a character");
    }

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

    protected void OnTriggerEnter(Collider collider)
    {
        CharacterObject target = collider.gameObject.GetComponent<CharacterObject>();
        if (target == null)
        {
            return;
        }
        if (CanHit(target))
        {
            HitTarget(target);
            targetsHit.Add(target);
        }
    }
    public void Initialize(ProjectileData projectileData)
    {
        AbilityEffectData[] abilityEffectsData = projectileData.abilityEffectsData;
        abilityEffects = new AbilityEffect[abilityEffectsData.Length];
        for (int i = 0; i < abilityEffectsData.Length; i++)
        {
            AbilityEffectData data = abilityEffectsData[i];
            String typeString = data.type;
            Type type = Type.GetType(typeString);
            object[] args = { data };
            abilityEffects[i] = (AbilityEffect)Activator.CreateInstance(type, args);
        }
        speed = projectileData.speed;
    }

    public void Update()
    {
        if (hasFired)
        {
            transform.position += GetTargetDirection() * Time.deltaTime * speed;
            if (ShouldDestroy())
            {
                foreach(Transform child in transform)
                {
                    Destroy(child.gameObject);
                }
                Destroy(this);
            }
        }
    }
}
