using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using UnityEngine.UI;
using System;

public class HealthDisplayHUD : MonoBehaviour
{
    public GameObject target;
    private Health healthComponent;
    public Slider slider;
    private void Start()
    {
        target = Player.localPlayer.gameObject;
        healthComponent = target.GetComponent<Health>();
        healthComponent.OnHealthChanged += OnHealthChanged;
    }

    private void LateUpdate()
    {
    }

    public void OnHealthChanged(Health health, float amt)
    {
        slider.value = health.currentHealthPercent;
    }

}
