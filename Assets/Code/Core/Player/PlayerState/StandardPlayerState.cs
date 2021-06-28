using System;
using System.Collections;
using System.Collections.Generic;
using System.Timers;
using Code;
using Code.Ability;
using Code.Core.Enemy;
using UnityEngine;
using UnityEngine.Events;

public class StandardPlayerState : BasePlayerState
{
    [SerializeField] private DashAbility _dashAbility;
    [SerializeField] private ExtraJumpAbility _extraJumpAbility;
    [SerializeField] private MoveAbility _moveAbility;
    [SerializeField] private GameObjectFlipperAbility _flipperAbility;

    private Vector2 _move;
    private bool _preparedToJump;
    

    protected override void InitAbilities()
    {
        _dashAbility.Init(this);
        _extraJumpAbility.Init(this);
        _moveAbility.Init(this);
        _flipperAbility.Init(this);
    }

    public override void OnStateStartUse()
    {
    }

    public override void OnStateStopUse()
    {
    }

    public override void CharacterUpdate()
    {
        _move = new Vector2(Input.GetAxis("Horizontal"), 0);

        if(!_dashAbility.IsDashing)
            _flipperAbility.FaceTo(_move.x);
        
        if(Input.GetKeyDown(KeyCode.LeftShift))
            _dashAbility.StartDashing(_flipperAbility.IsFacingRight ? Vector2.right : Vector2.left);
        
        var isJumpButtonPressed = Input.GetButtonDown("Jump");
        if (isJumpButtonPressed)
        {
            _preparedToJump = true;
        }
    }

    public override void CharacterFixedUpdate()
    {
        if (_dashAbility.IsDashing)
        {
            _dashAbility.MoveWithDash();
        }
        else
        {
            if (IsOnGround)
                _extraJumpAbility.UpdateExtraJumps();
            
            if (_preparedToJump)
            {
                _extraJumpAbility.Jump();
                _preparedToJump = false;
            }
            
            _moveAbility.Move(_move);
        }
    }
}