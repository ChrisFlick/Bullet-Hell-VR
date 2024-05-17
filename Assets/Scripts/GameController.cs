using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField] private Transform _flyingSaucerPrefab;
    [SerializeField] private Transform[] _spawnPoints;
    [SerializeField] private float _spawnTime;

    private float _timeSinceLastSpawn = 0;

    private int score = 0;

    private void Start()
    {
        FlyingSaucer.OnAnyHit += FlyingSaucer_OnAnyHit;
        BulletKillTrigger.OnAnyDodged += BulletKillTrigger_OnAnyDodged;
    }

    private void Update()
    {
        _timeSinceLastSpawn += Time.deltaTime;

        if (_timeSinceLastSpawn < _spawnTime) return;

        _timeSinceLastSpawn = 0;

        int spawnPointIndex = UnityEngine.Random.Range(0, _spawnPoints.Length);

        Instantiate(_flyingSaucerPrefab, _spawnPoints[spawnPointIndex]);
    }

    private void FlyingSaucer_OnAnyHit(object sender, EventArgs e)
    {
        score += 1000;

        Debug.Log($"Score is now {score}");
    }

    private void BulletKillTrigger_OnAnyDodged(object sender, EventArgs e)
    {
        // Add to score based on how many bullets are dodged.
        score += 1;
    }
}
