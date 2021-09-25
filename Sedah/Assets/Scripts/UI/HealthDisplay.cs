using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using UnityEngine.UI;
using System;

public class HealthDisplay : MonoBehaviour
{
    public GameObject target;
    private Health healthComponent;
    public Slider slider;
    private void Start()
    { 
        healthComponent = target.GetComponent<Health>();
        healthComponent.OnHealthChanged += OnHealthChanged;
        OnHealthChanged(healthComponent, 0f);
    }

    private void LateUpdate()
    {
        //TODO: Thid doesn't work rn because the player camera isn't the main camera
        transform.LookAt(transform.position + Camera.main.transform.forward);
    }

    public void OnHealthChanged(Health health, float amt)
    {
        slider.value = health.currentHealthPercent;
    }

}
