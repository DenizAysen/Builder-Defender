using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuildingTypeSelectUI : MonoBehaviour
{
    [SerializeField] private Sprite arrowSprite;
    [SerializeField] private List<BuildingTypeSO> ignoreBuildingTypeList;

    private Dictionary<BuildingTypeSO, Transform> btnTransfomDictionary;
    private Transform arrowBtn;
    private void Awake()
    {
        Transform btnTemplate = transform.Find("btnTemplate");
        btnTemplate.gameObject.SetActive(false);

        BuildingTypeListSO _buildingTypeList = Resources.Load<BuildingTypeListSO>(typeof(BuildingTypeListSO).Name);
        btnTransfomDictionary = new Dictionary<BuildingTypeSO, Transform>();

        int index = 0;
        arrowBtn = Instantiate(btnTemplate, transform);
        arrowBtn.gameObject.SetActive(true);

        float offsetAmount = 130f;

        arrowBtn.GetComponent<RectTransform>().anchoredPosition = new Vector2(offsetAmount * index, 0f);

        arrowBtn.Find("Image").GetComponent<Image>().sprite = arrowSprite;
        arrowBtn.Find("Image").GetComponent<RectTransform>().sizeDelta = new Vector2(0, -30);

        arrowBtn.GetComponent<Button>().onClick.AddListener(() =>
        {
            BuildingManager.Instance.SetActiveBuildingType(null);
        });
        index++;
        foreach (BuildingTypeSO buildingType in _buildingTypeList.list)
        {
            if (ignoreBuildingTypeList.Contains(buildingType)) continue;

            Transform btnTransform = Instantiate(btnTemplate, transform);
            btnTransform.gameObject.SetActive(true);

            offsetAmount = 130f;

            btnTransform.GetComponent<RectTransform>().anchoredPosition = new Vector2(offsetAmount * index, 0f);
            btnTransform.Find("Image").GetComponent<Image>().sprite = buildingType.sprite;

            btnTransform.GetComponent<Button>().onClick.AddListener(() =>
            {
                BuildingManager.Instance.SetActiveBuildingType(buildingType);
            });

            btnTransfomDictionary[buildingType] = btnTransform;

            index++;
        }
    }
    private void Start()
    {
        BuildingManager.Instance.OnActiveBuildingTypeChanged += OnActiveBuildingTypeChanged;
        UpdateActiveBuildingTypeButton();
    }

    private void OnActiveBuildingTypeChanged(BuildingTypeSO obj)
    {
        UpdateActiveBuildingTypeButton();
    }

    //private void Update()
    //{
    //    UpdateActiveBuildingTypeButton();
    //}
    private void UpdateActiveBuildingTypeButton()
    {
        arrowBtn.Find("selected").gameObject.SetActive(false);
        foreach (BuildingTypeSO buildingType in btnTransfomDictionary.Keys)
        {
            Transform btnTransform = btnTransfomDictionary[buildingType];
            btnTransform.Find("selected").gameObject.SetActive(false);
        }
        BuildingTypeSO activeBuildingType = BuildingManager.Instance.GetActiveBuildingType();
        if(activeBuildingType == null)
        {
            arrowBtn.Find("selected").gameObject.SetActive(true);
        }
        else
        {
            btnTransfomDictionary[activeBuildingType].Find("selected").gameObject.SetActive(true);
        }
    }
}
