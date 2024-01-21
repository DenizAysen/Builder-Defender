using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ResourceGeneratorOverlay : MonoBehaviour
{
    [SerializeField] private ResourceGenerator resourceGenerator;

    private Transform _barTransform;
    private void Start()
    {
        ResourceGeneratorData resourceGeneratorData = resourceGenerator.GetResourceGeneratorData();

        _barTransform = transform.Find("bar");
        transform.Find("icon").GetComponent<SpriteRenderer>().sprite = resourceGeneratorData.resourceType.sprite;
        transform.Find("text").GetComponent<TextMeshPro>().SetText(resourceGenerator.GetAmountGeneratedPerSecond().ToString("F1"));
    }
    private void Update()
    {
       _barTransform.localScale = new Vector3(1 - resourceGenerator.GetTimerNormalized(), 1f, 1f);
    }
}
