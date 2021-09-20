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

    // Events
    public static event Action<Health, float> onCharacterHealServer;
    public static event Action<Health, DamageReport> onCharacterDamageServer;

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
        float healReduction = this.character.GetStatusValue(StatusType.HealingReduced);
        float healPower = healerCharacter.GetStatValue(StatType.HealBuff);
        float netHealModifier = Mathf.Max(healPower - healReduction, -100);

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

    public float TakeDamage(DamageInfo damageInfo)
    {
        if (!NetworkServer.active)
        {
            Debug.LogWarning("[Server] function Health::TakeDamage called on client");
            return 0f;
        }
        if (!this.alive || this.invulnerable)
        {
            return 0f;
        }
        // Get overall damage modifiers
        CharacterObject attacker = damageInfo.attacker.GetComponent<CharacterObject>();
        float damageBuff = attacker.GetStatValue(StatType.DamageBuff);
        float damageReduction = this.character.GetStatValue(StatType.DamageReduction);
        float netDamageModifier = Mathf.Max(damageBuff - damageReduction, -100);

        float num = damageInfo.damage * (1f + (netDamageModifier * 0.01f));

        // Calculate reduction from resistence
        switch (damageInfo.damageType)
        {
            case DamageType.True:
                break;
            case DamageType.Physical:
                float armor  = this.character.GetStatValue(StatType.Armor);
                float armorPen = Math.Min(attacker.GetStatValue(StatType.ArmPen), 100f);
                armor *= (1f - (0.01f * armorPen));
                num *= (1f / (1f + (0.01f * armor)));
                break;
            case DamageType.Magic:
                float magicRes = this.character.GetStatValue(StatType.MagicRes);
                float magicPen = Math.Min(attacker.GetStatValue(StatType.MagicPen), 100f);
                magicRes *= (1f - (0.01f * magicPen));
                num *= (1f / (1f + (0.01f * magicRes)));
                break;
        }

        // Actually take the final value
        DamageReport damageReport = new DamageReport(damageInfo, num);
        this.currentHealth = Math.Max(this.currentHealth - num, 0f);
        this.lastHitTime = Time.time;
        Action<Health, DamageReport> action = Health.onCharacterDamageServer;
        if (action != null)
        {
            action(this, damageReport);
        }
        return num;
    }

    // Start is called before the first frame update
    void Awake()
    {
        currentHealth = maxHealth;
        this.character = base.GetComponent<CharacterObject>();
        this.lastHitTime = float.NegativeInfinity;
    }
}
