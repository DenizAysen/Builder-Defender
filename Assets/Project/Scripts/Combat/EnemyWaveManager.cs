using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWaveManager : MonoBehaviour
{
    public Action OnWaveNumberChanged;
    private enum WaveState
    {
        WaitingToSpawnNextWave,
        SpawningWave,

    }

    #region Serialized Fields
    [SerializeField] private List<Transform> spawnPositionTransformList;
    [SerializeField] private Transform nextWaveSpawnPositionTransform; 
    #endregion

    #region Privates
    private WaveState _waveState;
    private Vector3 _spawnPosition;

    private float _nextWaveSpawnTimer;
    private float _nextEnemySpawnTimer;

    private int _remainingEnemySpawnAmount;
    private int _waveNumber;
    #endregion
    public static EnemyWaveManager Instance { get; private set; }
    private void Awake()
    {
        if(Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }
    private void Start()
    {
        _waveState = WaveState.WaitingToSpawnNextWave;
        _spawnPosition = spawnPositionTransformList[UnityEngine.Random.Range(0, spawnPositionTransformList.Count)].position;
        nextWaveSpawnPositionTransform.position = _spawnPosition;
        _nextWaveSpawnTimer = 3f;
    }
    private void Update()
    {
        switch (_waveState)
        {
            case WaveState.WaitingToSpawnNextWave:
                _nextWaveSpawnTimer -= Time.deltaTime;
                if (_nextWaveSpawnTimer < 0f)
                {
                    SpawnWave();
                }
                break;

            case WaveState.SpawningWave:
                if (_remainingEnemySpawnAmount > 0)
                {
                    _nextEnemySpawnTimer -= Time.deltaTime;
                    if (_nextEnemySpawnTimer < 0f)
                    {
                        _nextEnemySpawnTimer = UnityEngine.Random.Range(0f, .2f);
                        Enemy.Create(_spawnPosition + UtilsClass.GetRandomDir() * UnityEngine.Random.Range(0f, 10f));
                        _remainingEnemySpawnAmount--;

                        if(_remainingEnemySpawnAmount <= 0)
                        {
                            _waveState = WaveState.WaitingToSpawnNextWave;
                            _spawnPosition = spawnPositionTransformList[UnityEngine.Random.Range(0, spawnPositionTransformList.Count)].position;
                            nextWaveSpawnPositionTransform.position = _spawnPosition;
                            _nextWaveSpawnTimer = 15f;
                        }
                    }
                }
                break;
        }      
    }
    private void SpawnWave()
    {
        _remainingEnemySpawnAmount = 3 + 2 * _waveNumber;
        _waveState = WaveState.SpawningWave;
        _waveNumber++;
        OnWaveNumberChanged?.Invoke();
    }
    public int GetWaveNumber() => _waveNumber;
    public float GetNextWaveSpawnTimer() => _nextWaveSpawnTimer;
    public Vector3 GetSpawnPosition() => _spawnPosition;
}
