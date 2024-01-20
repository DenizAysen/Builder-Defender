using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpritePositionScriptOrder : MonoBehaviour
{
    [SerializeField] private bool runOnce;
    [SerializeField] private float positionOffsetY;

    private SpriteRenderer _spriteRenderer;
    private float _precisionMultiplier;
    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _precisionMultiplier = 5f;
    }
    private void LateUpdate()
    {
        _spriteRenderer.sortingOrder = (int)(-(transform.position.y + positionOffsetY) * _precisionMultiplier);
        if (runOnce)
            Destroy(this);
    }
}
