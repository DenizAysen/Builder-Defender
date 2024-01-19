using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ResourcesUI : MonoBehaviour
{
    ResourceTypeListSO resourceTypeList;
    private Dictionary<ResourceTypeSO, Transform> resourceTypeTransformDictionary;
    private void Awake()
    {
        resourceTypeList = Resources.Load<ResourceTypeListSO>(typeof(ResourceTypeListSO).Name);
        resourceTypeTransformDictionary = new Dictionary<ResourceTypeSO, Transform>();

        Transform resourceTemplate = transform.Find("ResourceTemplate");
        resourceTemplate.gameObject.SetActive(false);

        int index = 0;
        foreach (ResourceTypeSO resourceType in resourceTypeList.list)
        {
            Transform resourceTransform = Instantiate(resourceTemplate, transform);
            resourceTransform.gameObject.SetActive(true);

            float offsetAmount = -160f;

            resourceTransform.GetComponent<RectTransform>().anchoredPosition = new Vector2(offsetAmount * index, 0f);
            resourceTransform.Find("Resource Image").GetComponent<Image>().sprite = resourceType.sprite;

            resourceTypeTransformDictionary[resourceType] = resourceTransform;
            index++;
        }
    }
    private void Start()
    {
        UpdateResourceAmount();
        ResourceManager.Instance.OnResourceAmountChanged += OnResourceAmountChanged;
    }

    private void OnResourceAmountChanged()
    {
        UpdateResourceAmount();
    }

    private void UpdateResourceAmount()
    {
        foreach (ResourceTypeSO resourceType in resourceTypeList.list)
        {
            Transform resourceTransform = resourceTypeTransformDictionary[resourceType];
            int resourceAmount = ResourceManager.Instance.GetResourceAmount(resourceType);
            resourceTransform.Find("Text").GetComponent<TextMeshProUGUI>().SetText(resourceAmount.ToString());
        }
    }
}
