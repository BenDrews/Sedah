using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
[System.Serializable]
[CreateAssetMenu(fileName = "TargetProjectile", menuName = "ScriptableObjects/Abilities/TargetProjectile")]
public class TargetProjectile: AbilityData
{
    public GameObject projectilePrefab;
    public ProjectileData projectileData;
    public Vector3 spawnOffset;
    public float fireEffectDuration;
    public override void Activate(CharacterObject attacker, CharacterObject target)
    {
        Vector3 projectileSpawn = attacker.gameObject.transform.position + spawnOffset;
        GameObject projectileObj = Instantiate<GameObject>(projectilePrefab, projectileSpawn, Quaternion.identity);
        NetworkServer.Spawn(projectileObj);
        CharacterTargetedProjectile firebolt = projectileObj.GetComponent<CharacterTargetedProjectile>();
        firebolt.Initialize(projectileData);
        firebolt.owner = attacker;
        firebolt.target = target;
        firebolt.Fire();
    }
}