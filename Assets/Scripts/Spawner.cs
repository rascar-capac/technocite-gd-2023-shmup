using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    // -- FIELDS

    [SerializeField] private EnemyBehaviour _enemyPrefab;
    [SerializeField] private float _spawnPeriod = 5f;
    [SerializeField] private Transform _enemyContainer;
    [SerializeField] private Transform _player;

    private List<Transform> _spawnPositions = new List<Transform>();
    private float _nextSpawnTime;

    // -- METHODS

    private void CheckSpawning()
    {
        if(Time.time >= _nextSpawnTime)
        {
            SpawnEnemy();
            _nextSpawnTime = Time.time + _spawnPeriod;
        }
    }

    private void SpawnEnemy()
    {
        Vector3 randomPosition = _spawnPositions[Random.Range(0, _spawnPositions.Count)].position;
        EnemyBehaviour enemy = Instantiate(_enemyPrefab, randomPosition, Quaternion.identity, _enemyContainer);
        enemy.Initialize(_player);
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
