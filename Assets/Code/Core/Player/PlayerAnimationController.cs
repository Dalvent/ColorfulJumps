using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class PlayerAnimationController : MonoBehaviour
{
    private static readonly int IS_MOVING = Animator.StringToHash("IsMoving");
    private static readonly int IS_DASHING = Animator.StringToHash("IsDashing");
    private static readonly int IS_JUMPING = Animator.StringToHash("IsJumping");
    private static readonly int KILLED_TRIGGER = Animator.StringToHash("Killed");
    private static readonly int START_FALLING_TRIGGER = Animator.StringToHash("StartFalling");

    private Animator _animator;

    private void Start()
    {
        _animator = GetComponent<Animator>();
    }

    public void OnStartDashing()
    {
        _animator.SetBool(IS_DASHING, true);
    }
    public void OnStopDashing()
    {
        _animator.SetBool(IS_DASHING, false);
    }

    public void OnJump()
    {
        _animator.SetBool(IS_JUMPING, true);
    }

    public void OnStartFalling()
    {
        _animator.SetBool(IS_JUMPING, false);
    }

    public void OnStartMoving()
    {
        _animator.SetBool(IS_MOVING, true);
    }
    
    public void OnStopMoving()
    {
        _animator.SetBool(IS_MOVING, false);
    }

    public void OnKilled()
    {
        _animator.SetTrigger(KILLED_TRIGGER);
    }
}