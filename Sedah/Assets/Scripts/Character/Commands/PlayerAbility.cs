using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

[RequireComponent(typeof(CharacterObject))]
[RequireComponent(typeof(EntityStateMachine))]
public class PlayerAbility : AbilityCast
{

    // Update is called once per frame
    public new void Update()
    {
        if (hasAuthority)
        {
            if (Input.GetKeyDown(KeyCode.Q) || Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.E))
            {
                int i = -1;
                if (Input.GetKeyDown(KeyCode.Q))
                {
                    i = 0;
                }
                else if (Input.GetKeyDown(KeyCode.W))
                {
                    i = 1;
                }
                else if (Input.GetKeyDown(KeyCode.E))
                {
                    i = 2;
                }
                CharacterObject character = this.GetComponent<CharacterObject>();
                Ability ability = character.GetAbility(i);

                if (ability == null)
                {
                    return;
                }

                EntityStateMachine stateMachine = GetComponent<EntityStateMachine>();
                RaycastHit hit;
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

                //Checks to see if target is self, enemy, ally, or point.
                if (ability.GetTarget() == TargetingType.Self)
                {
                    CmdSetAbilityCastStateTarget(this.gameObject, i);
                }
                else if (ability.GetTarget() == TargetingType.Enemy)
                {
                    LayerMask layerMask = LayerMask.GetMask("TargetableCharacters");
                    if (Physics.Raycast(ray, out hit, Mathf.Infinity, layerMask))
                    {
                        GameObject obj = hit.transform.gameObject;
                        // Tag comparison with enemy
                        if (obj.GetComponent<CharacterObject>()?.team != character.team)
                        {
                            float dist = Vector3.Distance(transform.position, obj.transform.position);
                            if (dist <= ability.GetRange())
                            {
                                CmdSetAbilityCastStateTarget(obj, i);
                            }
                        }
                    }
                }
                else if (ability.GetTarget() == TargetingType.Ally)
                {
                    LayerMask layerMask = LayerMask.GetMask("TargetableCharacters");
                    if (Physics.Raycast(ray, out hit, Mathf.Infinity, layerMask))
                    {
                        GameObject obj = hit.transform.gameObject;
                        if (obj.GetComponent<CharacterObject>()?.team == character.team)
                        {
                            float dist = Vector3.Distance(transform.position, obj.transform.position);
                            if (dist <= ability.GetRange())
                            {
                                Debug.Log(obj);
                                CmdSetAbilityCastStateTarget(obj, i);
                            }
                        }
                    }
                }
                else if (ability.GetTarget() == TargetingType.Point)
                {
                    LayerMask layerMask = LayerMask.GetMask("Terrain");
                    if (Physics.Raycast(ray, out hit, Mathf.Infinity, layerMask))
                    {
                        float dist = Vector3.Distance(transform.position, hit.point);
                        if (dist <= ability.GetRange())
                        {
                            CmdSetAbilityCastStateHit(hit.point, i);
                        }
                    }
                }
            }
        }
        base.Update();
    }
}
