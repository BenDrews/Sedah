using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

[RequireComponent(typeof(CharacterObject))]
public class PlayerAbility : NetworkBehaviour
{
    private CharacterObject character;

    private void Start()
    {
        if (NetworkClient.localPlayer.netId == base.netId)
        {
            this.character = base.GetComponent<CharacterObject>();
        }
        else
        {
            Destroy(this);
        }

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            Debug.Log("TRY");
            Ability ability = character.GetAbility(0).GetComponent<Ability>();
         
            if (!ability.IsOnCooldown() && character.GetStatValue(StatType.Mana) > ability.GetCost())
            {
                Debug.Log("TRY2");
                RaycastHit hit;
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                Debug.Log(Input.mousePosition);
                LayerMask layerMask = LayerMask.GetMask("TargetableCharacters");
                if (Physics.Raycast(ray, out hit, Mathf.Infinity, layerMask))
                {
                    GameObject obj = hit.transform.gameObject;
                    if (obj.GetComponent<Health>() != null)
                    {
                        float dist = Vector3.Distance(transform.position, obj.transform.position);
                        if (dist <= this.character.GetStatValue(StatType.AttackRange))
                        {
                            Debug.Log("TRY3");
                            CmdActivate(ability, gameObject, obj);
                        }
                    }
                }
            }

        }
    }

    [Command]
    public void CmdActivate(Ability ability, GameObject character, GameObject gameObj)
    {
        ability.Activate(character, gameObj);
    }
}
