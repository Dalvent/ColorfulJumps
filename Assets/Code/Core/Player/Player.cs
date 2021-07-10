using System;
using System.Collections;
using System.Collections.Generic;
using System.Timers;
using Code;
using Code.Core;
using Code.Core.Enemy;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(SpriteRenderer))]
public class Player : BoxCharacter
{
    [SerializeField] private UnityEvent _dieUnityEvent;
    [SerializeField] private UnityEvent _changeState;
    
    [SerializeField] private StandardPlayerState _standardPlayerState;
    [SerializeField] private GravityDashPlayerState _gravityDashPlayerState;

    [SerializeField] private PlayerState _currentStateEnum;
    
    [SerializeField] private List<SpriteRenderer> _spritesToChangeColor;

    public BasePlayerState CurrentState
    {
        get
        {
            switch (_currentStateEnum)
            {
                case PlayerState.Standard:
                    return _standardPlayerState;
                case PlayerState.GravityDash:
                    return _gravityDashPlayerState;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }

    public bool IsDead { get; private set; }
    protected override void Start()
    {
        InitStates();
        base.Start();
        
        ApplyCurrentState();
    }

    private void InitStates()
    {
        _standardPlayerState.Init(this);
        _gravityDashPlayerState.Init(this);
    }

    public void Kill(KillerType killerType)
    {
        if(IsDead)
            return;
        
        _dieUnityEvent.Invoke();   
        Rigidbody2D.Sleep();
        IsDead = true;
        GameManager.Instance.GameOver();
    }

    public void ChangeState(PlayerState state)
    {
        CurrentState.OnStateStopUse();
        _currentStateEnum = state;
        ApplyCurrentState();
        _changeState.Invoke();
    }

    private void ApplyCurrentState()
    {
        foreach (var spriteToChangeColor in _spritesToChangeColor)
        {
            spriteToChangeColor.color = CurrentState.OnSetColor;
        }
        CurrentState.OnStateStartUse();
    }
    
    private void Update()
    {
        if(IsDead || GameManager.Instance.GameEnd)
            return;
        
        CurrentState.CharacterUpdate();
    }

    protected override void FixedUpdate()
    {
        base.FixedUpdate();
        if(IsDead || GameManager.Instance.GameEnd)
            return;
        
        CurrentState.CharacterFixedUpdate();
    }
}