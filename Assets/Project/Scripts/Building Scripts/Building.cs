using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Building : MonoBehaviour
{
    private HealthSystem _healthSystem;
    private void Awake()
    {
        _healthSystem = GetComponent<HealthSystem>();
    }
    private void Start()
    {
        SubscribeEvents();
    }

    private void SubscribeEvents()
    {
        _healthSystem.OnDied += OnDied;
    }

    private void OnDied()
    {
        Destroy(gameObject);
    }
    private void OnDisable()
    {
        UnSubscribeEvents();
    }

    private void UnSubscribeEvents()
    {
        _healthSystem.OnDied -= OnDied;
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            _healthSystem.Damage(999);
        }
    }
}
