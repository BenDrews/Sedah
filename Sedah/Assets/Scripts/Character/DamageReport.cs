using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageReport
{
    public DamageInfo damageInfo;
    public float finalValue;

    public DamageReport(DamageInfo damageInfo, float finalValue)
    {
        this.damageInfo = damageInfo;
        this.finalValue = finalValue;
    }
}
