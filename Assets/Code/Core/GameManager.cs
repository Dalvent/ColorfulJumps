using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.Tilemaps;

public class GameManager : MonoBehaviour
{
    [SerializeField] private UnityEvent _onLevelPassed;
    [SerializeField] private UnityEvent _onGameOver;
    [SerializeField] private Tilemap _tilemap;
    [SerializeField] private Player _player;
    [SerializeField] private float _speedMultiplayerPerSecond;

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
    
    void OnEnable()
    {
        if (Instance != null && Instance != this)
        {
            Debug.LogError($"On scene two or more instance of {nameof(GameManager)!}");
        }

        Instance = this;
    }

    void OnDisable()
    {
        if (Instance == this)
            Instance = null;
    }

    public void FinishLevel()
    {
        _onLevelPassed.Invoke();
    }

    public void GameOver()
    {
        GameEnd = true;
        
        _onGameOver.Invoke();
    }
    
    public void RestartLevel()
    {
        SceneManager.LoadScene(0);
    }
}