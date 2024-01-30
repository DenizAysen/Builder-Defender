using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthSystem : MonoBehaviour
{
    public Action OnDamaged;
    public Action OnHealed;
    public Action OnDied;
    public Action OnHealthAmountMaxChanged;

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
    public bool IsFullHealth() => _healthAmount == healthAmountMax;
    public int GetHealthAmount() => _healthAmount;
    public int GetHealthAmountMax() => healthAmountMax;
    public float GetHealthAmountNormalized() => (float)_healthAmount / healthAmountMax;
    public void SetHealthAmountMax(int healthAmountMax, bool updateHealthAmount)
    {
        this.healthAmountMax = healthAmountMax;

        if (updateHealthAmount)
        {
            _healthAmount = healthAmountMax;
        }
        OnHealthAmountMaxChanged?.Invoke();
    }
    public void Heal(int healAmount)
    {
        _healthAmount += healAmount;
        _healthAmount = Mathf.Clamp(_healthAmount, 0, healthAmountMax);

        OnHealed?.Invoke();
    }
    public void HealFull()
    {
        _healthAmount = healthAmountMax;

        OnHealed?.Invoke();
    }
}
