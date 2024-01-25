using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class EnemyWaveUI : MonoBehaviour
{
    [SerializeField] private EnemyWaveManager waveManager;

    private TextMeshProUGUI _waveNumberText;
    private TextMeshProUGUI _waveMessageText;

    private RectTransform _enemyWaveSpawnPositionIndicator;
    private RectTransform _enemyClosestPositionIndicator;
    private Camera _camera;

    private float _nextWaveSpawnTimer;
    private float _distanceToNextSpawnPosition;
    private float _distanceToClosestEnemy;

    private Vector3 _dirToNextSpawnPosition;
    private Vector3 _dirToClosestEnemy;
    private void Awake()
    {
        _waveNumberText = transform.Find("waveNumberText").GetComponent<TextMeshProUGUI>();
        _waveMessageText = transform.Find("waveMessageText").GetComponent<TextMeshProUGUI>();
        _enemyWaveSpawnPositionIndicator = transform.Find("enemyWaveSpawnPositionIndicator").GetComponent<RectTransform>();
        _enemyClosestPositionIndicator = transform.Find("enemyClosestPositionIndicator").GetComponent<RectTransform>();
    }
    private void Start()
    {
        _camera = Camera.main;
        waveManager.OnWaveNumberChanged += OnWaveNumberChanged;
        SetWaveNumberText("Wave " + waveManager.GetWaveNumber());
    }

    private void OnWaveNumberChanged()
    {
        SetWaveNumberText("Wave " + waveManager.GetWaveNumber());
    }

    private void Update()
    {
        HandeNextWaveMessage();
        HandleEnemyWaveSpawnPositionIndicator();
        HandleClosestEnemyPositionIndicator();
    }
    private void OnDisable()
    {
        waveManager.OnWaveNumberChanged -= OnWaveNumberChanged;
    }
    private void HandeNextWaveMessage()
    {
        _nextWaveSpawnTimer = waveManager.GetNextWaveSpawnTimer();
        if (_nextWaveSpawnTimer <= 0f)
        {
            SetMessageText("");
        }
        else
        {
            SetMessageText("Next Wave in " + _nextWaveSpawnTimer.ToString("F1") + "s");
        }
    }
    private void HandleEnemyWaveSpawnPositionIndicator()
    {
        _dirToNextSpawnPosition = (waveManager.GetSpawnPosition() - _camera.transform.position).normalized;

        _enemyWaveSpawnPositionIndicator.anchoredPosition = _dirToNextSpawnPosition * 300f;
        _enemyWaveSpawnPositionIndicator.eulerAngles = new Vector3(0, 0, UtilsClass.GetAngleFromVector(_dirToNextSpawnPosition));

        _distanceToNextSpawnPosition = Vector3.Distance(waveManager.GetSpawnPosition(), _camera.transform.position);
        _enemyWaveSpawnPositionIndicator.gameObject.SetActive(_distanceToNextSpawnPosition > _camera.orthographicSize * 1.5f);
    }
    private void HandleClosestEnemyPositionIndicator()
    {
        float _targetMaxRadius = 9999f;
        Collider2D[] colliders = Physics2D.OverlapCircleAll(_camera.transform.position, _targetMaxRadius);
        Enemy _targetEnemy = null;
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

        if (_targetEnemy != null)
        {
            _dirToClosestEnemy = (_targetEnemy.transform.position - _camera.transform.position).normalized;

            _enemyClosestPositionIndicator.anchoredPosition = _dirToClosestEnemy * 250f;
            _enemyClosestPositionIndicator.eulerAngles = new Vector3(0, 0, UtilsClass.GetAngleFromVector(_dirToClosestEnemy));

            _distanceToClosestEnemy = Vector3.Distance(_targetEnemy.transform.position, _camera.transform.position);
            _enemyClosestPositionIndicator.gameObject.SetActive(_distanceToClosestEnemy > _camera.orthographicSize * 1.5f);
        }

        else
        {
            _enemyClosestPositionIndicator.gameObject.SetActive(false);
        }
    }
    private void SetMessageText(string message)
    {
        _waveMessageText.SetText(message);
    }
    private void SetWaveNumberText(string waveNumber)
    {
        _waveNumberText.SetText(waveNumber);
    }
}
