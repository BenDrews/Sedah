using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class BlockadeShield : Ability
{
    //IGNORE, WIP
    public GameObject shield; 
    public override void Activate(CharacterObject self, CharacterObject target, AbilityEffect[] abilityEffects)
    {
        //layer for ability priority.
        self.gameObject.layer = 8;
        foreach (AbilityEffect effect in abilityEffects)
        {
            effect.Invoke(self.GetComponent<CharacterObject>(), self.GetComponent<CharacterObject>());
        }
    }
}
