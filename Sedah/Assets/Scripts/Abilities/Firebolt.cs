using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
[System.Serializable]
public class Firebolt : Ability
{
    [SerializeField] protected GameObject fireEffect;
    public GameObject projectilePrefab;
    public Vector3 spawnOffset;
    public float fireEffectDuration;
    public override void Activate(GameObject attacker, GameObject target)
    {
        Vector3 projectileSpawn = transform.position + spawnOffset;
        GameObject projectileObj = Instantiate<GameObject>(projectilePrefab, projectileSpawn, Quaternion.identity);
        CharacterTargetedProjectile firebolt = projectileObj.GetComponent<CharacterTargetedProjectile>();
        firebolt.owner = attacker.GetComponent<CharacterObject>();
        firebolt.target = target.GetComponent<CharacterObject>();
        firebolt.Fire();

        GameObject effect = Instantiate(fireEffect);
        effect.transform.position = attacker.transform.position + spawnOffset;
        NetworkServer.Spawn(effect);
        StartCoroutine(DestroyEffect(effect));
    }

    public IEnumerator DestroyEffect(GameObject effect)
    {
        yield return new WaitForSeconds(1f);
        Destroy(effect);
    }
}