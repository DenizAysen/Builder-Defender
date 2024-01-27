using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingConstruction : MonoBehaviour
{
    public static BuildingConstruction Create(Vector3 position , BuildingTypeSO buildingType)
    {
        Transform buildingConstructionPrefab = Resources.Load<Transform>("BuildingConstruction");
        Transform buildingConstructionTransform = Instantiate(buildingConstructionPrefab, position, Quaternion.identity);

        BuildingConstruction buildingConstruction = buildingConstructionTransform.GetComponent<BuildingConstruction>();
        buildingConstruction.SetBuildingType(buildingType);

        return buildingConstruction;
    }
    private BuildingTypeSO _buildingType;
    private BoxCollider2D _boxCollider2D;

    private SpriteRenderer _spriteRenderer;

    private BuildingTypeHolder _buildingTypeHolder;

    private Material _constructionMaterial;

    private float _constractionTimer;
    private float _constractionTimerMax;
    private void Awake()
    {
        _boxCollider2D = GetComponent<BoxCollider2D>();
        _spriteRenderer = transform.Find("sprite").GetComponent<SpriteRenderer>();
        _buildingTypeHolder = GetComponent<BuildingTypeHolder>();
        _constructionMaterial = _spriteRenderer.material;
    }
    private void Update()
    {
        _constractionTimer -= Time.deltaTime;

        _constructionMaterial.SetFloat("_Progress", GetConstructionTimerNormalized());
        if(_constractionTimer <= 0f)
        {
            Instantiate(_buildingType.prefab, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }
    private void SetBuildingType(BuildingTypeSO buildingType)
    {
        _buildingType = buildingType;
        _constractionTimerMax = _buildingType.constructionTimerMax;
        _constractionTimer = _constractionTimerMax;
        _spriteRenderer.sprite = buildingType.sprite;
        _buildingTypeHolder.buildingType = buildingType;

        _boxCollider2D.offset = buildingType.prefab.GetComponent<BoxCollider2D>().offset;
        _boxCollider2D.size = buildingType.prefab.GetComponent<BoxCollider2D>().size;
    }
    public float GetConstructionTimerNormalized() => 1 - _constractionTimer / _constractionTimerMax;
}
