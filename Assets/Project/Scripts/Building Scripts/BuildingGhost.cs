using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingGhost : MonoBehaviour
{
    public GameObject spriteGameObject;
    #region Unity Methods
    private void Awake()
    {
        spriteGameObject = transform.Find("sprite").gameObject;

        Hide();
    }
    private void Start()
    {
        SubscribeEvents();
    }
    private void Update()
    {
        transform.position = UtilsClass.GetMouseWorldPosition();
    }
    #endregion
    #region Event Subscribtion
    private void SubscribeEvents()
    {
        BuildingManager.Instance.OnActiveBuildingTypeChanged += OnActiveBuildingTypeChanged;
    }

    private void OnActiveBuildingTypeChanged(BuildingTypeSO buildingType)
    {
        BuildingTypeSO _activeBuildingType = buildingType;
        if (_activeBuildingType == null)
            Hide();
        else
            Show(_activeBuildingType.sprite);
    } 
    #endregion
    private void Show(Sprite ghostSprite)
    {
        spriteGameObject.SetActive(true);
        spriteGameObject.GetComponent<SpriteRenderer>().sprite = ghostSprite;
    }
    private void Hide() => spriteGameObject.SetActive(false);
}
