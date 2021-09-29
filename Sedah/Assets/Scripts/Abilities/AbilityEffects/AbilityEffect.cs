using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AbilityEffect
{
    public abstract void Invoke(CharacterObject owner, CharacterObject target);
}
