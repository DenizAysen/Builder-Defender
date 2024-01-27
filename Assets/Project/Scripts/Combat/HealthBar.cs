using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private HealthSystem healthSystem;
    private Transform _barTransform;
    private void Awake()
    {
        _barTransform = transform.Find("bar");
    }
    private void Start()
    {
        SubscribeEvents();
        UpdateBar();
        UpdateHealthBarVisible();
    }

    private void SubscribeEvents()
    {
        healthSystem.OnDamaged += OnDamaged;
        healthSystem.OnHealed += OnHealed;
    }

    private void OnHealed()
    {
        UpdateBar();
        UpdateHealthBarVisible();
    }

    private void OnDamaged()
    {
        UpdateBar();
        UpdateHealthBarVisible();
    }

    private void UpdateBar()
    {
        _barTransform.localScale = new Vector3(healthSystem.GetHealthAmountNormalized(), 1f, 1f);
    }
    private void UpdateHealthBarVisible()
    {
        if (healthSystem.IsFullHealth())
        {
            gameObject.SetActive(false);
        }
        else
        {
            gameObject.SetActive(true);
        }
    }
}
