using UnityEngine;
using UnityEngine.Events;

public class GameController : MonoBehaviour
{
    [SerializeField] private UnityEvent _onGameEnd;
    
    [SerializeField] private PlayerController _playerController;

    public static GameController Instance { get; private set; }

    public PlayerController PlayerController => _playerController;
    
    void OnEnable()
    {
        if (Instance != null && Instance != this)
        {
            Debug.LogError($"On scene two or more instance of {nameof(GameController)!}");
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
        _onGameEnd.Invoke();
        _playerController.DisableInput();
    }
}