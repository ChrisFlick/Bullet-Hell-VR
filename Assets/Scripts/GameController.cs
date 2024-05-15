using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField] private Transform _flyingSaucerPrefab;
    [SerializeField] private Transform[] _spawnPoints;
    [SerializeField] private float _spawnTime;

    private float _timeSinceLastSpawn = 0;

    private void Update()
    {
        _timeSinceLastSpawn += Time.deltaTime;

        if (_timeSinceLastSpawn < _spawnTime) return;

        _timeSinceLastSpawn = 0;

        int spawnPointIndex = Random.Range(0, _spawnPoints.Length);

        Instantiate(_flyingSaucerPrefab, _spawnPoints[spawnPointIndex]);
    }
}
