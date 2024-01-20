using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceGenerator : MonoBehaviour
{
    private List<ResourceNode> _nearResourceNodes;
    private ResourceGeneratorData _resourceGeneratorData;

    private float _timer;
    private float _timerMax;
    private void Awake()
    {
        _resourceGeneratorData = GetComponent<BuildingTypeHolder>().buildingType.resourceGeneratorData;
        _timerMax = _resourceGeneratorData.timerMax;
        _nearResourceNodes = new List<ResourceNode>();
    }
    private void Start()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, _resourceGeneratorData.resourceDetectionRadius);
        int nearbyResourceAmount = 0;
        foreach (Collider2D collider in colliders)
        {
            ResourceNode resourceNode = collider.GetComponent<ResourceNode>();

            if (resourceNode != null)
            {
                if(resourceNode.resourceType == _resourceGeneratorData.resourceType)
                {
                _nearResourceNodes.Add(resourceNode);
                nearbyResourceAmount++;
                }
            }
        }
        nearbyResourceAmount = Mathf.Clamp(nearbyResourceAmount, 0, _resourceGeneratorData.maxResourceAmount);

        if (nearbyResourceAmount == 0)
            enabled = false;
        else
        {
            _timerMax = (_resourceGeneratorData.timerMax / 2f) +
                _resourceGeneratorData.timerMax * (1 - (float)nearbyResourceAmount / _resourceGeneratorData.maxResourceAmount);
        }


        Debug.Log("nearbyResourceAmount : " + nearbyResourceAmount + " ; timerMax :"+_timerMax);
    }
    private void Update()
    {
        _timer -= Time.deltaTime;
        if (_timer <= 0f)
        {
            _timer = _timerMax;
            Debug.Log("Ding "+_resourceGeneratorData.resourceType.nameString);
            ResourceManager.Instance.AddResource(_resourceGeneratorData.resourceType, 1);
        }
    }
}
