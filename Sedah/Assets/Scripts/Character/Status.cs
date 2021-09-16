using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Status", menuName = "ScriptableObjects/Status")]
public class Status : ScriptableObject
{
    [SerializeField] private string id;

    public string ID
    {
        get { return id; }
        private set { id = value; }
    }
}
