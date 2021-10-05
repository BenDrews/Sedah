using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using UnityEngine.UI;
using System;

public class AbilityTemplate : NetworkBehaviour
{
    public string abilityButtonAxisName = "Fire1";
    public Image darkMask;
    public Text coolDownTextDisplay;

    private Ability ability;
    private GameObject effect;
    private Sprite myButtonImage;
    private AudioSource abilitySource;
    private float cooldownTime;
    private bool onCooldown;
    private float range;
    public TargetingType target;
    private float nextReadyTime;
    private float coolDownTimeLeft;
    private GameObject specialEffect;

    [HideInInspector]
    public AbilityEffect[] abilityEffects;

    void Start()
    {
    }

    public void Initialize(Ability selectedAbility)
    {
        ability = selectedAbility;
        abilitySource = GetComponent<AudioSource>();
        myButtonImage = ability.sprite;
        cooldownTime = ability.cooldownTime;
        specialEffect = ability.specialEffect;
        target = ability.target;
        range = ability.range;

        AbilityEffectData[] abilityEffectsData = ability.abilityEffectsData;
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

    public void Activate(CharacterObject player, CharacterObject target)
    {
        if (abilityEffects.Length > 0)
        {
            ability.Activate(player, target, abilityEffects);
        }
        else
        {
            ability.Activate(player, target);
        }

        if (ability.hasDash)
        {
            if (abilityEffects.Length > 0)
            {
                StartCoroutine(ability.ActivateDash(player, target, abilityEffects));
            }
            else
            {
                StartCoroutine(ability.ActivateDash(player, target));
            }
        }

        if (specialEffect != null)
        {
            GameObject effect = Instantiate(specialEffect);
            NetworkServer.Spawn(effect);
            StartCoroutine(DestroyEffect(effect));
        }
    }

    public void Activate(CharacterObject player, Vector3 point)
    {
        if (abilityEffects.Length > 0)
        {
            ability.Activate(player, point, abilityEffects);
        }
        else
        {
            ability.Activate(player, point);
        }

        if (specialEffect != null)
        {
            GameObject effect = Instantiate(specialEffect);
            NetworkServer.Spawn(effect);
            StartCoroutine(DestroyEffect(effect));
        }
    }

    public IEnumerator DestroyEffect(GameObject effect)
    {
        yield return new WaitForSeconds(2f);
        NetworkServer.Destroy(effect);
    }

    public bool OnCooldown()
    {
        return onCooldown;
    }

    public void SetCooldown(bool cd)
    {
        onCooldown = cd;
    }

    public float GetCooldown()
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

    public Sprite GetSprite()
    {
        return myButtonImage;
    }
}
