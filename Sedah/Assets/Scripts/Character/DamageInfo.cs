using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageInfo
{
    public float damage;
    public GameObject attacker;
    public Vector3 position;
    public DamageType damageType;
    public bool isCrit;
    public bool isDoT;
    public bool isSpell;
    public bool isFraction;

    public DamageInfo(float damage, GameObject attacker, Vector3 position, DamageType damageType) {
        this.damage = damage;
        this.attacker = attacker;
        this.position = position;
        this.damageType = damageType;
    }
}
