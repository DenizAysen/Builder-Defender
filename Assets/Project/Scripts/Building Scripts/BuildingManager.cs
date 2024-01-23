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
            {
                if (CanSpawnBuilding(_activeBuildingType, UtilsClass.GetMouseWorldPosition(), out string errorMessage))
                {
                    if (ResourceManager.Instance.CanAffordCost(_activeBuildingType.constractionResourceCostArray))
                    {
                        ResourceManager.Instance.SpendResources(_activeBuildingType.constractionResourceCostArray);
                        Instantiate(_activeBuildingType.prefab, UtilsClass.GetMouseWorldPosition(), Quaternion.identity);
                    }
                    else
                    {
                        ToolTipUI.Instance.Show("Cannot afford " + _activeBuildingType.GetConstractionResourceString(),
                            new ToolTipUI.ToolTipTimer { timer = 2f });
                    }
                }
                else
                {
                    ToolTipUI.Instance.Show(errorMessage, new ToolTipUI.ToolTipTimer { timer = 2f});
                }
            }
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
    private bool CanSpawnBuilding(BuildingTypeSO buildingType , Vector3 position, out string errorMessage)
    {
        BoxCollider2D boxCollider2D = buildingType.prefab.GetComponent<BoxCollider2D>();

        Collider2D[] colliders = Physics2D.OverlapBoxAll(position + (Vector3)boxCollider2D.offset, boxCollider2D.size, 0f);

        bool _isAreaClear = colliders.Length == 0;
        if (!_isAreaClear)
        {
            errorMessage = "Area is not clear!";
            return false;
        }

        colliders = Physics2D.OverlapCircleAll(position, buildingType.minConstractionRadius);

        foreach (Collider2D collider2D in colliders)
        {
            BuildingTypeHolder buildingTypeHolder = collider2D.GetComponent<BuildingTypeHolder>();
            if(buildingTypeHolder != null)
            {
                if(buildingTypeHolder.buildingType == buildingType)
                {
                    errorMessage = "Too close to another building of the same type!";
                    return false;
                }
            } 
        }

        float maxConstractionRadius = 25f;
        colliders = Physics2D.OverlapCircleAll(position, maxConstractionRadius);

        foreach (Collider2D collider2D in colliders)
        {
            BuildingTypeHolder buildingTypeHolder = collider2D.GetComponent<BuildingTypeHolder>();
            if (buildingTypeHolder != null)
            {
                errorMessage = "";
                return true;
            }
        }

        errorMessage = "Too far from any other building!";
        return false;
    }
}
