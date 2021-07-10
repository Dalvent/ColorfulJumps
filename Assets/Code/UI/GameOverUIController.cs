using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using Code.Core.Event;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOverUIController : MonoBehaviour
{
    [SerializeField] private Text _scoreText;
    [SerializeField] private Text _bestScoreText;

    public void Start()
    {
        gameObject.SetActive(false);
    }

    public void ShowGameOver(GameOverEvent gameEndEvent)
    {
        gameObject.SetActive(true);
        if (!gameEndEvent.IsNewBestScore)
        {
            _bestScoreText.text = $"Best score: {gameEndEvent.BeastScore.ToString("F", CultureInfo.InvariantCulture)}";
            _scoreText.text = $"Your score:\n{gameEndEvent.CurrentScore.ToString("F", CultureInfo.InvariantCulture)}";
        }
        else
        {
            _bestScoreText.enabled = false;
            _scoreText.text = $"Your NEW best score: {gameEndEvent.CurrentScore.ToString("F", CultureInfo.InvariantCulture)}";
        }
    }

    public void OnRestartClick()
    {
        SceneManager.LoadScene("MainGameScene");
    }

    public void OnBackClick()
    {
        
    }
}
