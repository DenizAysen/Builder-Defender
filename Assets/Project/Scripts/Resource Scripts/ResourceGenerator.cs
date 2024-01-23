using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceGenerator : MonoBehaviour
{
    
    #region Privates
    private List<ResourceNode> _nearResourceNodes;
    private ResourceGeneratorData _resourceGeneratorData;

    private float _timer;
    private float _timerMax;
    #endregion
    
    #region Unity Methods
    private void Awake()
    {
        _resourceGeneratorData = GetComponent<BuildingTypeHolder>().buildingType.resourceGeneratorData;
        _timerMax = _resourceGeneratorData.timerMax;
        _nearResourceNodes = new List<ResourceNode>();
    }
    private void Start()
    {
        int nearbyResourceAmount = GetNearbyResourceAmount(_resourceGeneratorData,transform.position);

        if (nearbyResourceAmount == 0)
            enabled = false;
        else
        {
            _timerMax = (_resourceGeneratorData.timerMax / 2f) +
                _resourceGeneratorData.timerMax * (1 - (float)nearbyResourceAmount / _resourceGeneratorData.maxResourceAmount);
        }

    }
    private void Update()
    {
        _timer -= Time.deltaTime;
        if (_timer <= 0f)
        {
            _timer = _timerMax;
            Debug.Log("Ding " + _resourceGeneratorData.resourceType.nameString);
            ResourceManager.Instance.AddResource(_resourceGeneratorData.resourceType, 1);
        }
    } 
    #endregion
    public ResourceGeneratorData GetResourceGeneratorData()
    {
        return _resourceGeneratorData;
    }
    public float GetTimerNormalized() => _timer / _timerMax;
    public float GetAmountGeneratedPerSecond() => 1 / _timerMax;
    public static int GetNearbyResourceAmount(ResourceGeneratorData resourceGeneratorData, Vector3 position)
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(position, resourceGeneratorData.resourceDetectionRadius);
        int nearbyResourceAmount = 0;
        foreach (Collider2D collider in colliders)
        {
            ResourceNode resourceNode = collider.GetComponent<ResourceNode>();

            if (resourceNode != null)
            {
                if (resourceNode.resourceType == resourceGeneratorData.resourceType)
                {
                    //_nearResourceNodes.Add(resourceNode);
                    nearbyResourceAmount++;
                }
            }
        }
        nearbyResourceAmount = Mathf.Clamp(nearbyResourceAmount, 0, resourceGeneratorData.maxResourceAmount);
        return nearbyResourceAmount;
    }
}
