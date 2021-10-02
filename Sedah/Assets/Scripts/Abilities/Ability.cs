using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TargetingType
{
    Self,
    Enemy,
    Ally,
    Point
}
public abstract class Ability: ScriptableObject
{
    [SerializeField] protected int id;
    public bool hasDash;
    public GameObject specialEffect;
    [SerializeField] public Sprite sprite;
    [SerializeField] public float range = 0;
    [SerializeField] public TargetingType target;
    [SerializeField] public int cooldownTime = 0;
    [SerializeField, HideInInspector] protected bool onCooldown = false;
    public AbilityEffectData[] abilityEffectsData;


    public virtual void Activate(CharacterObject self, CharacterObject target, AbilityEffect[] abilityEffects)
    {

    }

    public virtual void Activate(CharacterObject self, Vector3 point, AbilityEffect[] abilityEffects)
    {

    }

    public virtual void Activate(CharacterObject self, CharacterObject target)
    {

    }

    public virtual void Activate(CharacterObject self, Vector3 point)
    {

    }

    public virtual IEnumerator ActivateDash(CharacterObject self, CharacterObject target, AbilityEffect[] abilityEffects)
    {
        yield return new WaitForSeconds(0.01f);
    }

    public virtual IEnumerator ActivateDash(CharacterObject self, CharacterObject target)
    {
        yield return new WaitForSeconds(0.01f);
    }

}