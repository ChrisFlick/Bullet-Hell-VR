using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    const float GAME_LENGTH = 60;

    [SerializeField] private Transform _flyingSaucerPrefab;
    [SerializeField] private Transform[] _spawnPoints;
    [SerializeField] private float _spawnTime;

    private float _timeSinceLastSpawn = 0;
    private float _gameTime = 0;

    private int _score = 0;
    private int _highScore;

    private void Start()
    {
        FlyingSaucer.OnAnyHit += FlyingSaucer_OnAnyHit;
        BulletKillTrigger.OnAnyDodged += BulletKillTrigger_OnAnyDodged;

        _timeSinceLastSpawn = _spawnTime; // This ensures a Flying Saucer spawns at the start of the game.
    }

    private void Update()
    {
        _gameTime += Time.deltaTime;

        if (_gameTime > GAME_LENGTH)
        {
            int mainMenuIndex = 0;
            SceneManager.LoadScene(mainMenuIndex, LoadSceneMode.Single);
        }

        _timeSinceLastSpawn += Time.deltaTime;

        if (_timeSinceLastSpawn < _spawnTime) return;

        _timeSinceLastSpawn = 0;

        int spawnPointIndex = UnityEngine.Random.Range(0, _spawnPoints.Length);

        Instantiate(_flyingSaucerPrefab, _spawnPoints[spawnPointIndex]);
    }

    private void OnEnable()
    {
        _highScore = PlayerPrefs.GetInt("highScore");
    }

    void OnDisable()
    {
        PlayerPrefs.SetInt("score", _score);
        
        if (_score > _highScore)
        {
            PlayerPrefs.SetInt("highScore", _score);
        }
    }

    private void FlyingSaucer_OnAnyHit(object sender, EventArgs e)
    {

        _score += 1000;

        Debug.Log($"Score is now {_score}");
    }

    private void BulletKillTrigger_OnAnyDodged(object sender, EventArgs e)
    {

        // Add to score based on how many bullets are dodged.
        _score += 1;
    }
}
