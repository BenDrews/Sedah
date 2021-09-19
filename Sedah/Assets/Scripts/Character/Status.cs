using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum StatusType
{
    Burning,
    HealingReduced,
    Poisoned
}

[CreateAssetMenu(fileName = "Status", menuName = "ScriptableObjects/Status")]

public class Status : ScriptableObject
{
    [SerializeField] private string id;
    [SerializeField] private StatusType statusType;
    [SerializeField] private float _value;
    public string ID
    {
        get { return id; }
        private set { id = value; }
    }

    public StatusType StatusType
    {
        get { return statusType; }
        private set { statusType = value; }
    }

    public float Value
    {
        get { return _value; }
        private set { _value = value; }
    }
}
