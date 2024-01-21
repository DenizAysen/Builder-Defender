using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ResourceNearbyOverlay : MonoBehaviour
{
    private ResourceGeneratorData _resourceGeneratorData;
    private int _nearbyResourceAmount;
    private float _percent;
    private TextMeshPro _text;
    private void Awake()
    {
        Hide();
    }
    private void Start()
    {
        _text = transform.Find("text").GetComponent<TextMeshPro>();
    }
    private void Update()
    {
        _nearbyResourceAmount = ResourceGenerator.GetNearbyResourceAmount(_resourceGeneratorData, transform.position);
        _percent = Mathf.RoundToInt((float)_nearbyResourceAmount / _resourceGeneratorData.maxResourceAmount * 100f);
        _text.SetText(_percent + "%");
    }
    public void Show(ResourceGeneratorData resourceGeneratorData)
    {
        _resourceGeneratorData = resourceGeneratorData;
        gameObject.SetActive(true);

        transform.Find("icon").GetComponent<SpriteRenderer>().sprite = _resourceGeneratorData.resourceType.sprite;       
    }
    public void Hide()
    {
        gameObject.SetActive(false);
    }
}
