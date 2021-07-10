using System;
using System.Collections;
using Code.Core.Event;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.Tilemaps;

public class GameManager : MonoBehaviour
{
    private const string BEST_SCORE = "BEST_SCORE";
    
    [SerializeField] private UnityEvent _onLevelPassed;
    [SerializeField] private UnityEvent<GameOverEvent> _onGameOver;
    [SerializeField] private Tilemap _tilemap;
    [SerializeField] private Player _player;
    [SerializeField] private float _speedMultiplayerPerSecond;
    [SerializeField] private float _scorePerPlayerMeters = 1f;
    [SerializeField] private float _scorePerPlayerSecond = 0.1f;
    
    private float maxPlayerLocationX = 0f;

    public float CurrentScore { get; set; }
    
    public Player Player => _player;
    
    public void ChangePlatformColor(Color color)
    {
        _tilemap.color = color;
    }
    
    private int _secondsRemaining;
    public int SecondsRemaining
    {
        get => _secondsRemaining;
        set
        {
            _secondsRemaining = value;
            SpeedMultiplayer = 1 + _speedMultiplayerPerSecond * _secondsRemaining;
        }
    }

    public float SpeedMultiplayer { get; set; } = 1;
    public bool GameEnd { get; set; } = false;
    public static GameManager Instance { get; private set; }
    
    
    void OnEnable()
    {
        if (Instance != null && Instance != this)
        {
            Debug.LogError($"On scene two or more instance of {nameof(GameManager)!}");
        }

        Instance = this;
    }

    private void Start()
    {
        StartCoroutine(GameTimerCoroutine());
    }

    private IEnumerator GameTimerCoroutine()
    {
        while (!GameEnd)
        {
            yield return new WaitForSeconds(1);
            SecondsRemaining++;
        }
    }
    
    void OnDisable()
    {
        if (Instance == this)
            Instance = null;
    }

    public void Update()
    {
        CurrentScore += _scorePerPlayerSecond * Time.deltaTime;
        var currentPlayerX = _player.transform.position.x;
        if (currentPlayerX > maxPlayerLocationX)
        {
            CurrentScore += (currentPlayerX - maxPlayerLocationX) * _scorePerPlayerMeters;
            maxPlayerLocationX = currentPlayerX;
        }
    }

    public void FinishLevel()
    {
        _onLevelPassed.Invoke();
    }

    public void GameOver()
    {
        GameEnd = true;
        
        var bestScore = PlayerPrefs.GetFloat(BEST_SCORE);
        var isNewBestScore = false;
        if (bestScore < CurrentScore)
        {
            bestScore = CurrentScore;
            PlayerPrefs.SetFloat(BEST_SCORE, bestScore);
            isNewBestScore = true;
        }
        
        _onGameOver.Invoke(new GameOverEvent()
        {
            BeastScore = bestScore,
            CurrentScore = CurrentScore,
            IsNewBestScore = isNewBestScore
        });
    }
    
    public void RestartLevel()
    {
        SceneManager.LoadScene(0);
    }
}