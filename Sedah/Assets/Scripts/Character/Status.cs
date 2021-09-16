using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum StatusType
{
    Poisoned,
    Burning
}

[CreateAssetMenu(fileName = "Status", menuName = "ScriptableObjects/Status")]

public class Status : ScriptableObject
{
    [SerializeField] private string id;
    [SerializeField] private StatusType statusType;
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
}
