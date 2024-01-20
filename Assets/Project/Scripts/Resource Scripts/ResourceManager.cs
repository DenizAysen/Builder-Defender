using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceManager : MonoBehaviour
{
    private Dictionary<ResourceTypeSO, int> _resourceAmountDictionary;
    private ResourceTypeListSO _resourceTypeList;

    public Action OnResourceAmountChanged;
    public static ResourceManager Instance { get; private set; }
    private void Awake()
    {
        if(Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;

        _resourceAmountDictionary = new Dictionary<ResourceTypeSO, int>();
        _resourceTypeList = Resources.Load<ResourceTypeListSO>(typeof(ResourceTypeListSO).Name);

        foreach (ResourceTypeSO resourceType in _resourceTypeList.list)
        {
            _resourceAmountDictionary[resourceType] = 0;
        }
        TestLogResourceAmountDictionary();
    }
    private void TestLogResourceAmountDictionary()
    {
        foreach (ResourceTypeSO resourceType in _resourceAmountDictionary.Keys)
        {
            Debug.Log(resourceType.nameString + ": " + _resourceAmountDictionary[resourceType]);
        }
    }
    public void AddResource(ResourceTypeSO resourceType, int amount)
    {
        _resourceAmountDictionary[resourceType] += amount;
        OnResourceAmountChanged?.Invoke();
        TestLogResourceAmountDictionary();
    }
    public int GetResourceAmount(ResourceTypeSO resourceType) => _resourceAmountDictionary[resourceType];
}
