using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthSystem : MonoBehaviour
{
    public Action OnDamaged;
    public Action OnDied;

    [SerializeField] private int healthAmountMax;
    private int _healthAmount;
    private void Awake()
    {
        _healthAmount = healthAmountMax;
    }
    public void Damage(int damageAmount)
    {
        _healthAmount -= damageAmount;
        _healthAmount = Mathf.Clamp(_healthAmount, 0, healthAmountMax);

        OnDamaged?.Invoke();

        if (IsDead())
            OnDied?.Invoke();
    }
    public bool IsDead() => _healthAmount == 0;
    public int GetHealthAmount() => _healthAmount;
    public float GetHealthAmountNormalized() => (float)_healthAmount / healthAmountMax;
}
