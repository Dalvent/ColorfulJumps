using System;
using Code.Core.Enemy;
using UnityEngine;
using UnityEngine.Events;

public class PlayerStateChanger : MonoBehaviour
{
    [SerializeField] private UnityEvent _onPick;
    [SerializeField] private PlayerState _stateToChange;

    public void OnTriggerEnter2D(Collider2D other)
    {
        var player = other.GetComponent<Player>();
        if (player == null)
            return;

        _onPick.Invoke();
        player.ChangeState(_stateToChange);
        Destroy(gameObject);
    }
}