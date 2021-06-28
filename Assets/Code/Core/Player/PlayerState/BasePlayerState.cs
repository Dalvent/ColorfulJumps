using System;
using Code.Ability;
using Code.Core.Enemy;
using UnityEngine;

public abstract class BasePlayerState : BaseCharacter
{
    [SerializeField] private Color _onSetColor = Color.white;
    private Player _player;

    public void Init(Player player)
    {
        _player = player;
    }

    private void Start()
    {
        InitAbilities();
    }

    protected abstract void InitAbilities();

    public Color OnSetColor => _onSetColor;
    public Player Player => _player;
    public sealed override bool IsOnGround => _player.IsOnGround;
    public sealed override Rigidbody2D Rigidbody2D => _player.Rigidbody2D;

    public abstract void OnStateStartUse();

    public abstract void OnStateStopUse();

    public abstract void CharacterUpdate();
    public abstract void CharacterFixedUpdate();
}