using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingManager : MonoBehaviour
{
    #region Privates
    private Camera _mainCamera;
    private BuildingTypeSO _buildingType;
    private BuildingTypeListSO _buildingTypeList;

    private Vector3 _mouseWorldPosition;
    #endregion

    #region Unity Methods
    private void Start()
    {
        _mainCamera = Camera.main;

        _buildingTypeList = Resources.Load<BuildingTypeListSO>(typeof(BuildingTypeListSO).Name);
        _buildingType = _buildingTypeList.list[0];
    }
    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Instantiate(_buildingType.prefab, GetMouseWorldPosition(), Quaternion.identity);
        }
        if (Input.GetKeyDown(KeyCode.T))
        {
            _buildingType = _buildingTypeList.list[0];
        }
        if (Input.GetKeyDown(KeyCode.Y))
        {
            _buildingType = _buildingTypeList.list[1];
        }
    } 
    #endregion
    private Vector3 GetMouseWorldPosition()
    {
        _mouseWorldPosition = _mainCamera.ScreenToWorldPoint(Input.mousePosition);
        _mouseWorldPosition.z = 0f;
        return _mouseWorldPosition;
    }
}
