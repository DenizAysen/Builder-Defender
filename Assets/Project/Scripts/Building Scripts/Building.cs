using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Building : MonoBehaviour
{
    private HealthSystem _healthSystem;
    private BuildingTypeSO _buildingType;
    private Transform _buildingDemolishBtn;
    private Transform _buildingRepairBtn;
    private void Awake()
    {
        _buildingType = GetComponent<BuildingTypeHolder>().buildingType;
        _healthSystem = GetComponent<HealthSystem>();
        _buildingDemolishBtn = transform.Find("BuildingDemolishBtn");
        _buildingRepairBtn = transform.Find("BuildingRepairBtn");

        HideBuildingDemolishBtn();
        HideBuildingRepairBtn();

        _healthSystem.SetHealthAmountMax(_buildingType.healthAmountMax , true);
    }
    private void Start()
    {
        SubscribeEvents();
    }

    private void SubscribeEvents()
    {
        _healthSystem.OnDied += OnDied;
        _healthSystem.OnDamaged += OnDamaged;
        _healthSystem.OnHealed += OnHealed;
    }

    private void OnHealed()
    {
        if (_healthSystem.IsFullHealth())
            HideBuildingRepairBtn();
    }

    private void OnDamaged()
    {
        ShowBuildingRepairBtn();
        SoundManager.Instance.PlaySound(SoundManager.Sound.BuildingDamaged);
        CinemachineShake.Instance.ShakeCamera(7f, .15f);
        ChromacticAberrationEffect.Instance.SetWeight(1f);
    }

    private void OnDied()
    {
        Instantiate(GameAssets.Instance.buildingDestroyedParticlesPrefab, transform.position, Quaternion.identity);
        Destroy(gameObject);
        SoundManager.Instance.PlaySound(SoundManager.Sound.BuildingDestroyed);
        CinemachineShake.Instance.ShakeCamera(10f, .2f);
        ChromacticAberrationEffect.Instance.SetWeight(1f);
    }
    private void OnDisable()
    {
        UnSubscribeEvents();
    }

    private void UnSubscribeEvents()
    {
        _healthSystem.OnDied -= OnDied;
    }
    private void OnMouseEnter()
    {
        ShowBuildingDemolishBtn();
    }
    private void OnMouseExit()
    {
        HideBuildingDemolishBtn();
    }
    private void ShowBuildingDemolishBtn()
    {
        if (_buildingDemolishBtn != null)
            _buildingDemolishBtn.gameObject.SetActive(true);
    }
    private void HideBuildingDemolishBtn()
    {
        if (_buildingDemolishBtn != null)
            _buildingDemolishBtn.gameObject.SetActive(false);
    }
    private void ShowBuildingRepairBtn()
    {
        if (_buildingRepairBtn != null)
            _buildingRepairBtn.gameObject.SetActive(true);
    }
    private void HideBuildingRepairBtn()
    {
        if (_buildingRepairBtn != null)
            _buildingRepairBtn.gameObject.SetActive(false);
    }
}
