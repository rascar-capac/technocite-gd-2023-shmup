using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    // -- FIELDS

    [SerializeField] private AnimationCurve _spawnPeriodCurve;
    [SerializeField] private float _maxSpawnTimeInSeconds = 120f;
    [SerializeField] private EnemyBehaviour _simpleEnemyPrefab;
    [SerializeField] private EnemyBehaviour _eliteEnemyPrefab;
    [SerializeField][Range(0f, 1f)] private float _eliteProbabilityRatio = 0.1f;
    [SerializeField] private Transform _enemyContainer;
    [SerializeField] private LifeHolder _player;
    [SerializeField] private GameManager _gameManager;

    private List<Transform> _spawnPositions = new List<Transform>();
    private float _nextSpawnTime;

    // -- METHODS

    private void CheckSpawning()
    {
        if(Time.time >= _nextSpawnTime)
        {
            SpawnEnemy();
            _nextSpawnTime = Time.time + _spawnPeriodCurve.Evaluate(Time.time / _maxSpawnTimeInSeconds);
        }
    }

    private void SpawnEnemy()
    {
        Vector3 randomPosition = _spawnPositions[Random.Range(0, _spawnPositions.Count)].position;
        bool mustSpawnElite = Random.value < _eliteProbabilityRatio;
        EnemyBehaviour enemy = Instantiate(mustSpawnElite ? _eliteEnemyPrefab : _simpleEnemyPrefab, randomPosition, Quaternion.identity, _enemyContainer);
        enemy.Initialize(_player, _gameManager);
    }

    // -- UNITY

    private void Awake()
    {
        foreach(Transform child in transform)
        {
            _spawnPositions.Add(child);
        }
    }

    private void Update()
    {
        CheckSpawning();
    }
}
