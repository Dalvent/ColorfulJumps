using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Player : MonoBehaviour
{
    private Rigidbody2D _rigidbody;
    private SpriteRenderer _spriteRenderer;
    
    [Header("Movement")]
    [SerializeField] private float _speed;
    [SerializeField] private float _jumpForce;
    [SerializeField] private int _extraJumpCount = 1;
    [SerializeField] private int _dashTime;
    [SerializeField] private int _dashSpeed;
    [SerializeField] private int _dashDelay;

    [Header("Ground check")]
    [SerializeField] private Transform _groundCheck;
    [SerializeField] private float _checkLineLenght;
    [SerializeField] private LayerMask _groundLayerMask;
    
    private Vector2 _move;
    private int _currentExtraJumpCount;
    private bool _preparedToJump = false;
    private bool _facingRight = true;
    private Vector2 _dashDirection;
    private int _currentDashTime = 0;

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _spriteRenderer = GetComponent<SpriteRenderer>();

        _currentExtraJumpCount = _extraJumpCount;
    }

    public void Jump()
    {
        if(!_preparedToJump)
            _preparedToJump = Input.GetButtonDown("Jump");
    }

    public void Move(Vector2 move)
    {
        _move = move;
    }

    private void FixedUpdate()
    {
        if (CheckIsStayOnGround())
            UpdateExtraJumps();

        if (_preparedToJump)
        {
            if (CanJump())
                LaunchUp();
        }

        if(NeedFaceRight())
            Flip();
        else if(NeedFaceLeft())
            Flip();

        MoveByMoveVector();
    }

    private bool CanDash()
    {
        return _currentDashTime != 0;
    }

    private void MoveWithDash()
    {
        _rigidbody.velocity = _dashDirection * _dashSpeed;
        _currentDashTime--;
    }
    
    pprivate void 

    private void UpdateExtraJumps()
    {
        _currentExtraJumpCount = _extraJumpCount;
    }
    
    private bool CanJump()
    {
        return _currentExtraJumpCount != 0;
    }

    private void LaunchUp()
    {
        _rigidbody.velocity = Vector2.up * _jumpForce;
        _currentExtraJumpCount--;
        _preparedToJump = false;
    }
    
    private bool CheckIsStayOnGround()
    {
        return Physics2D.Raycast(
            _groundCheck.position,
            Vector2.down, 
            _checkLineLenght,
            _groundLayerMask);
    }
    
    private bool NeedFaceRight()
    {
        return _move.x > 0 && _facingRight;
    }
    
    private bool NeedFaceLeft()
    {
        return _move.x > 0 && _facingRight;
    }
    
    private void MoveByMoveVector()
    {
        _rigidbody.velocity = new Vector2(_move.x * _speed, _rigidbody.velocity.y);
    }

    private void Flip()
    {
        _facingRight = !_facingRight;
        _spriteRenderer.flipX = !_facingRight;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(
            _groundCheck.position,
            (Vector2) _groundCheck.position + Vector2.down * _checkLineLenght
            );
    }
}
