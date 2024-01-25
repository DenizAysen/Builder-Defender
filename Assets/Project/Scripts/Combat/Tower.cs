using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
    [SerializeField]private float shootTimerMax;

    #region Privates
    private Enemy _targetEnemy;
    private float _targetMaxRadius;

    private float _lookForTargetTimer;
    private float _lookForTargetTimerMax;

    private float _shootTimer;

    private Vector3 _projectileSpawnPos; 
    #endregion
    private void Awake()
    {
        _targetMaxRadius = 20f;
        _lookForTargetTimerMax = .2f;
        _lookForTargetTimer = Random.Range(0f, _lookForTargetTimerMax);
        _projectileSpawnPos = transform.Find("ProjectileSpawnPos").position;
        _shootTimer = shootTimerMax;
    }
    private void Update()
    {
        HandleTargeting();
        HandleShooting();
    }
    private void LookForTargets()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, _targetMaxRadius);
        foreach (Collider2D collider in colliders)
        {
            Enemy enemy = collider.GetComponent<Enemy>();
            if (enemy != null)
            {
                if (_targetEnemy == null)
                {
                    _targetEnemy = enemy;
                }
                else
                {
                    if (Vector3.Distance(transform.position, enemy.transform.position) <
                        Vector3.Distance(transform.position, _targetEnemy.transform.position))
                    {
                        _targetEnemy = enemy;
                    }
                }
            }
        }       
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
    private void HandleShooting()
    {
        _shootTimer -= Time.deltaTime;
        if(_shootTimer < 0f)
        {
            if (_targetEnemy != null)
            {
                ArrowProjectile.Create(_projectileSpawnPos, _targetEnemy);
            }
            _shootTimer += shootTimerMax;
        }
        
    }
        
}
