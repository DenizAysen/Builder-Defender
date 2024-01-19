using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceGenerator : MonoBehaviour
{
    private BuildingTypeSO buildingTypeSO;

    private float timer;
    private float timerMax;
    private void Awake()
    {
        buildingTypeSO = GetComponent<BuildingTypeHolder>().buildingType;
        timerMax = buildingTypeSO.resourceGeneratorData.timerMax;
    }
    private void Update()
    {
        timer -= Time.deltaTime;
        if (timer <= 0f)
        {
            timer = timerMax;
            Debug.Log("Ding "+buildingTypeSO.resourceGeneratorData.resourceType.nameString);
            ResourceManager.Instance.AddResource(buildingTypeSO.resourceGeneratorData.resourceType, 1);
        }
    }
}
