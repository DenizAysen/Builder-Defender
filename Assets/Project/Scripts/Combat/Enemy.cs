using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    #region Privates
    private Rigidbody2D _rigidbody;
    private Transform _targetTransform;
    private HealthSystem _healthSystem;

    private Vector3 _moveDir;
    private float _moveSpeed;
    private float _targetMaxRadius;
    private float _lookForTargetTimer;
    private float _lookForTargetTimerMax;
    #endregion
    #region Unity Methods
    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _moveDir = Vector3.zero;
        _moveSpeed = 6f;
        _targetMaxRadius = 10f;
        _lookForTargetTimerMax = .2f;
        _lookForTargetTimer = Random.Range(0f, _lookForTargetTimerMax);
        _healthSystem = GetComponent<HealthSystem>();
    }
    private void Start()
    {
        if (BuildingManager.Instance.GetHQBuilding() != null)
            _targetTransform = BuildingManager.Instance.GetHQBuilding().transform;

        SubsribeEvents();
    }
    private void Update()
    {
        HandleMovement();

        HandleTargeting();
    }
    #endregion
    private void SubsribeEvents()
    {
        _healthSystem.OnDied += OnDied;
        _healthSystem.OnDamaged += OnDamaged;
    }

    private void OnDamaged()
    {
        SoundManager.Instance.PlaySound(SoundManager.Sound.EnemyHit);
        CinemachineShake.Instance.ShakeCamera(5f, .1f);
        ChromacticAberrationEffect.Instance.SetWeight(.5f);
    }

    private void OnDied()
    {
        SoundManager.Instance.PlaySound(SoundManager.Sound.EnemyDie);
        CinemachineShake.Instance.ShakeCamera(7f, .15f);
        ChromacticAberrationEffect.Instance.SetWeight(.5f);
        Instantiate(GameAssets.Instance.enemyDieParticlesPrefab, transform.position , Quaternion.identity);
        Destroy(gameObject);
    }
    private void OnDisable()
    {
        UnSubscribeEvents();
    }

    private void UnSubscribeEvents()
    {
        _healthSystem.OnDied -= OnDied;
        _healthSystem.OnDamaged -= OnDamaged;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Building building = collision.gameObject.GetComponent<Building>();
        
        if(building != null)
        {
            HealthSystem healthSystem = building.GetComponent<HealthSystem>();
            healthSystem.Damage(10);
            _healthSystem.Damage(999);
        }
    }
    public static Enemy Create(Vector3 position)
    {
        Transform enemyTransform = Instantiate(GameAssets.Instance.enemyPrefab, position, Quaternion.identity);
        Enemy enemy = enemyTransform.GetComponent<Enemy>();
        return enemy;
    }
    private void LookForTargets()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, _targetMaxRadius);
        foreach (Collider2D collider in colliders)
        {
            Building building = collider.GetComponent<Building>();
            if(building != null)
            {
                if(_targetTransform == null)
                {
                    _targetTransform = building.transform;
                }
                else
                {
                    if(Vector3.Distance(transform.position , building.transform.position) < 
                        Vector3.Distance(transform.position , _targetTransform.position))
                    {
                        _targetTransform = building.transform;
                    }
                }
            }
        }
        if(_targetTransform == null)
        {
            if(BuildingManager.Instance.GetHQBuilding() != null)
            {
                _targetTransform = BuildingManager.Instance.GetHQBuilding().transform;
            }
        }
    }
    private void HandleMovement()
    {
        if(_targetTransform != null)
        {
            _moveDir = (_targetTransform.position - transform.position).normalized;
            _rigidbody.velocity = _moveDir * _moveSpeed;
        }
        else
            _rigidbody.velocity = Vector2.zero;
    }
    private void HandleTargeting()
    {
        _lookForTargetTimer -= Time.deltaTime;
        if (_lookForTargetTimer < 0f)
        {
            _lookForTargetTimer = _lookForTargetTimerMax;
            LookForTargets();
        }
    }
}
