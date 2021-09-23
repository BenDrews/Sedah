using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using UnityEngine.UI;
using System;

public class HealthDisplay : NetworkBehaviour
{ 
    public Text healthText;

    private void Start()
    {
        if (isClient)
        {
            GameObject localPlayer = NetworkClient.localPlayer.gameObject;
            Health healthComponent = localPlayer.GetComponent<Health>();
            healthComponent.OnHealthChanged += OnHealthChanged;
        }
    }

    public void OnHealthChanged(Health health, float amt)
    {
        healthText.text = "HEALTH: " + Convert.ToInt32(health.currentHealth + 0.5f).ToString();
    }
}
