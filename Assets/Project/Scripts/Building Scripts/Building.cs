using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Building : MonoBehaviour
{
    private HealthSystem _healthSystem;
    private BuildingTypeSO _buildingType;
    private void Awake()
    {
        _buildingType = GetComponent<BuildingTypeHolder>().buildingType;
        _healthSystem = GetComponent<HealthSystem>();

        _healthSystem.SetHealthAmountMax(_buildingType.healthAmountMax , true);
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
}
