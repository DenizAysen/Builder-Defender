using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingGhost : MonoBehaviour
{
    public GameObject spriteGameObject;

    private ResourceNearbyOverlay resourceNearbyOverlay;
    #region Unity Methods
    private void Awake()
    {
        spriteGameObject = transform.Find("sprite").gameObject;
        resourceNearbyOverlay = transform.Find("ResourceNearbyOverlay").GetComponent<ResourceNearbyOverlay>();
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
        {
            Hide();
            resourceNearbyOverlay.Hide();
        }
        else
        {
            Show(_activeBuildingType.sprite);
            if (buildingType.hasResourceGeneratorData)
            {
                resourceNearbyOverlay.Show(_activeBuildingType.resourceGeneratorData);
            }
            else
            {
                resourceNearbyOverlay.Hide();
            }
        }
    }
    private void OnDisable()
    {
        UnSubscribeEvents();
    }

    private void UnSubscribeEvents()
    {
        BuildingManager.Instance.OnActiveBuildingTypeChanged -= OnActiveBuildingTypeChanged;
    }
    #endregion
    private void Show(Sprite ghostSprite)
    {
        spriteGameObject.SetActive(true);
        spriteGameObject.GetComponent<SpriteRenderer>().sprite = ghostSprite;
    }
    private void Hide() => spriteGameObject.SetActive(false);
}
