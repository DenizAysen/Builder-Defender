using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowProjectile : MonoBehaviour
{
    #region Privates
    private Enemy _targetEnemy;

    private Vector3 _moveDir;
    private Vector3 _lastMoveDir;

    private float _moveSpeed;
    private float _timeToDie = 2f;

    int _damageAmount;
    #endregion
    #region Unity Methods
    private void Awake()
    {
        _moveSpeed = 20f;
        _damageAmount = 10;
    }
    private void Update()
    {
        if (_targetEnemy != null)
        {
            _moveDir = (_targetEnemy.transform.position - transform.position).normalized;
            _lastMoveDir = _moveDir;
        }
        else
            _moveDir = _lastMoveDir;
        transform.position += _moveDir * _moveSpeed * Time.deltaTime;
        transform.eulerAngles = new Vector3(0, 0, UtilsClass.GetAngleFromVector(_moveDir));

        _timeToDie -= Time.deltaTime;
        if (_timeToDie < 0)
            Destroy(gameObject);
    } 
    #endregion
    private void SetTarget(Enemy targetEnemy)
    {
        _targetEnemy = targetEnemy;
    }
    #region Physics
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Enemy enemy = collision.gameObject.GetComponent<Enemy>();
        if(enemy != null)
        {
            enemy.GetComponent<HealthSystem>().Damage(_damageAmount);
            Destroy(gameObject);
        }
    }
    #endregion
    public static ArrowProjectile Create(Vector3 position , Enemy targetEnemy)
    {
        Transform arrowTransform = Instantiate(GameAssets.Instance.arrowProjectilePrefab, position, Quaternion.identity);

        ArrowProjectile arrow = arrowTransform.GetComponent<ArrowProjectile>();
        arrow.SetTarget(targetEnemy);

        return arrow;
    }
}
