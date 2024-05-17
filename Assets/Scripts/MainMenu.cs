using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _scoreText;
    [SerializeField] private TextMeshProUGUI _highScoreText;

    private int _highScore;

    private void OnEnable()
    {
        int score = PlayerPrefs.GetInt("score");

        _scoreText.text = $"Your score: {score}";

        int _highscore = PlayerPrefs.GetInt("highScore");

        if (score > _highScore)
        {
            _highScore = score;
        }

        if (_highscore == 0 )
        {
            _highscore = 1000; 
        }

        _highScoreText.text = $"High Score: {_highScore}";
    }

    private void OnDisable()
    {
        PlayerPrefs.SetInt("highScore", _highScore);
    }
}
