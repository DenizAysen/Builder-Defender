using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private HealthSystem healthSystem;

    private Transform _barTransform;
    private Transform _seperatorContainer;
    private void Awake()
    {
        _barTransform = transform.Find("bar");
    }
    private void Start()
    {
        _seperatorContainer = transform.Find("seperatorContainer");
        ConstructHealthBarSeperators();

        SubscribeEvents();
        UpdateBar();
        UpdateHealthBarVisible();
    }

    private void SubscribeEvents()
    {
        healthSystem.OnDamaged += OnDamaged;
        healthSystem.OnHealed += OnHealed;
        healthSystem.OnHealthAmountMaxChanged += OnHealthMaxAmountChanged;
    }

    private void OnHealthMaxAmountChanged()
    {
        ConstructHealthBarSeperators();
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
    private void ConstructHealthBarSeperators()
    {
        
        Transform _seperatorTemplate = _seperatorContainer.Find("seperatorTemplate");
        _seperatorTemplate.gameObject.SetActive(false);

        foreach (Transform seperator in _seperatorContainer)
        {
            if (seperator == _seperatorTemplate)
                continue;
            Destroy(seperator.gameObject);
        }

        int healthAmountPerSeperator = 10;
        float barSize = 3f;
        float barOneHealthAmountSize = barSize / healthSystem.GetHealthAmountMax();

        int healthSeperatorCount = Mathf.FloorToInt(healthSystem.GetHealthAmountMax() / healthAmountPerSeperator);
        for (int i = 1; i < healthSeperatorCount; i++)
        {
            Transform seperatorTransform = Instantiate(_seperatorTemplate, _seperatorContainer);
            seperatorTransform.gameObject.SetActive(true);
            seperatorTransform.localPosition = new Vector3(barOneHealthAmountSize * i * healthAmountPerSeperator, 0, 0);
        }
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
        gameObject.SetActive(true);
    }
}
