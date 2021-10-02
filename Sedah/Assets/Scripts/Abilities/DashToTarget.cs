using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using UnityEngine.AI;
using System;

[CreateAssetMenu(fileName = "DashToTarget", menuName = "ScriptableObjects/Abilities/DashToTarget")]
public class DashToTarget : Ability
{
    public override void Activate(CharacterObject self, CharacterObject target)
    {
        Debug.Log("I TRied");

    }

    public override IEnumerator ActivateDash(CharacterObject self, CharacterObject target, AbilityEffect[] abilityEffects)
    {
        Debug.Log("TRIED");
        NavMeshAgent agent = self.gameObject.GetComponent<NavMeshAgent>();
        agent.isStopped = true;
        float acc = 50f;
        float height = 0f;
        float midPoint = Vector3.Distance(self.gameObject.transform.position, target.gameObject.transform.position);
        Vector3 pos = self.gameObject.transform.position;
        Debug.Log(self);
        Debug.Log(target);
        while (Vector3.Distance(self.gameObject.transform.position, target.gameObject.transform.position) > 3)
        {
            Vector3 direction = (target.gameObject.transform.position - self.gameObject.transform.position).normalized;
            pos = self.gameObject.transform.position += direction * Time.deltaTime * acc;
            agent.Warp(pos);
            direction.y = 0;
            Quaternion rot = Quaternion.LookRotation(direction);

            self.gameObject.transform.rotation = Quaternion.Lerp(self.gameObject.transform.rotation, rot, 30 * Time.deltaTime);

            if (Vector3.Distance(self.gameObject.transform.position, target.gameObject.transform.position) > midPoint / 2)
            {
                acc += 5;
                height += 0.15f;
            }
            else
            {
                acc -= 5;
                height -= 0.15f;
            }
            Debug.Log("TRIED");
            yield return new WaitForSeconds(Time.deltaTime);
        }
        pos.y = 0;
        agent.Warp(pos);
        foreach(AbilityEffect effect in abilityEffects)
        {
            effect.Invoke(self, target);
            effect.Invoke(self, self);
        }
        self.GetComponent<Animator>().SetBool("Casting", false);

    }
}
