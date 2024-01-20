using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
public class BuildingManager : MonoBehaviour
{
    #region Privates
    private Camera _mainCamera;
    private BuildingTypeSO _activeBuildingType;
    private BuildingTypeListSO _buildingTypeList;

    private Vector3 _mouseWorldPosition;
    #endregion
    public Action<BuildingTypeSO> OnActiveBuildingTypeChanged;
    public static BuildingManager Instance { get; private set; }
    #region Unity Methods
    private void Awake()
    {
        if(Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }
    private void Start()
    {
        _mainCamera = Camera.main;

        _buildingTypeList = Resources.Load<BuildingTypeListSO>(typeof(BuildingTypeListSO).Name);
    }
    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && !EventSystem.current.IsPointerOverGameObject())
        {
            if(_activeBuildingType != null)
                Instantiate(_activeBuildingType.prefab, UtilsClass.GetMouseWorldPosition(), Quaternion.identity);
        }
        
    } 
    #endregion
    private Vector3 GetMouseWorldPosition()
    {
        _mouseWorldPosition = _mainCamera.ScreenToWorldPoint(Input.mousePosition);
        _mouseWorldPosition.z = 0f;
        return _mouseWorldPosition;
    }
    public void SetActiveBuildingType(BuildingTypeSO buildingType)
    {
        _activeBuildingType = buildingType;
        OnActiveBuildingTypeChanged?.Invoke(buildingType);
    }
    public BuildingTypeSO GetActiveBuildingType() => _activeBuildingType;
}
