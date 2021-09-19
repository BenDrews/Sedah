using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

[DisallowMultipleComponent]
[RequireComponent(typeof(CharacterObject))]
public class Health : NetworkBehaviour
{
    public float currentHealth;
    public CharacterObject character;
    public float lastHitTime;
    public bool alive
    {
        get
        {
            return this.currentHealth > 0f;
        }
    }

    public float maxHealth
    {
        get
        {
            return this.character.GetStat(StatType.Health).Value;
        }
    }

    public float missingHealth
    {
        get
        {
            return this.character.GetStat(StatType.Health).Value - currentHealth;
        }
    }

    public float timeSinceLastHit
    {
        get
        {
            return Time.fixedTime - this.lastHitTime;
        }
    }

    public bool invulnerable { get; set; }

    //Event 
    public static event Action<Health, float> onCharacterHealServer;

    public float Heal(float amount, GameObject healer = null, bool nonRegen = true)
    {
        if(!NetworkServer.active)
        {
            Debug.LogWarning("[Server] function Health::Heal called on client");
            return 0f;
        }
        if (!this.alive || amount <= 0f)
        {
            return 0f;
        }
        float num = this.currentHealth;

        CharacterObject healerCharacter = healer ? healer.GetComponent<CharacterObject>() : this.character;
        float healReduction = this.character.GetStatus(StatusType.HealingReduced).Value;
        float healPower = healerCharacter.HasStat(StatType.HealPower) ? healerCharacter.GetStat(StatType.HealPower).Value : 0f;
        float netHealModifier = Mathf.Max(Mathf.Min(healPower - healReduction, 100), -100);

        num *= (1f + (netHealModifier * 0.01f));
        this.currentHealth = Mathf.Min(this.maxHealth, this.currentHealth += num);
        if (nonRegen)
        {
            Action<Health, float> action = Health.onCharacterHealServer;
            if (action != null)
            {
                action(this, amount);
            }
        }
        return num;
    }

    public float HealFraction(float fraction, GameObject healer)
    {
        return this.Heal(fraction * this.maxHealth, healer, true);
    }

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
