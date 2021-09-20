using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

[DisallowMultipleComponent]
[RequireComponent(typeof(CharacterObject))]
public class Health : NetworkBehaviour
{
    [SyncVar]
    public float currentHealth;
    private CharacterObject character;
    public double lastHitTime;
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

    public double timeSinceLastHit
    {
        get
        {
            return NetworkTime.time - this.lastHitTime;
        }
    }

    public bool invulnerable { get; set; }

    // Events
    public static event Action<Health, float> onCharacterHealServer;
    public static event Action<Health, DamageReport> onCharacterDamageServer;
    public static event Action<Health, DamageReport> onCharacterDeathServer;

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
        float healReduction = this.character.GetTotalStatusValue(StatusType.HealingReduced);
        float healPower = healerCharacter.GetStatValue(StatType.HealBuff);
        float netHealModifier = Mathf.Max(healPower - healReduction, -100);

        num *= (1f + (netHealModifier * 0.01f));
        this.currentHealth = Mathf.Min(this.maxHealth, this.currentHealth += num);
        if (nonRegen)
        {
            Health.onCharacterHealServer(this, num);
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

        float num = damageInfo.isFraction ? damageInfo.damage * 0.01f * this.maxHealth : damageInfo.damage; 
        num *= (1f + (netDamageModifier * 0.01f));

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
        this.lastHitTime = NetworkTime.time;
        Health.onCharacterDamageServer(this, damageReport);

        if (this.currentHealth == 0f)
        {
            Health.onCharacterDeathServer(this, damageReport);
        }
        Debug.Log("DAMAGE TAKEN: " + num);
        return num;
    }

    // Start is called before the first frame update
    void Awake()
    {
        this.character = base.GetComponent<CharacterObject>();
        this.currentHealth = this.maxHealth;
        this.lastHitTime = float.NegativeInfinity;
    }

    public void FixedUpdate()
    {
        // TODO: Change this to do a heal per status so that healer can be properly attributed. Also the same for DoT
        float healingOverTime = this.character.GetTotalStatusValue(StatusType.HealingOverTime);
        Heal(healingOverTime * Time.deltaTime);
    }
}
