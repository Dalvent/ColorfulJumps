using System;
using System.Collections.Generic;
using Code.Ability;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(BoxCollider2D))]
public class BoxCharacter : BaseCharacter
{
    [Header("Ground check")]
    [SerializeField] private float _checkLenght = 0.5f;
    [SerializeField] private LayerMask _groundLayerMask;
    
    private Rigidbody2D _rigidbody2D;
    private BoxCollider2D _boxCollider2D;
    private bool _isOnGround;

    public override bool IsOnGround => _isOnGround;
    public override Rigidbody2D Rigidbody2D => _rigidbody2D;

    protected virtual void Start()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _boxCollider2D = GetComponent<BoxCollider2D>();
    }
    protected virtual void FixedUpdate()
    {
        _isOnGround = CheckIsStayOnGround();
    }
    
    private bool CheckIsStayOnGround()
    {
        return Physics2D.BoxCast(_boxCollider2D.bounds.center, new Vector2(_boxCollider2D.size.x * 0.75f, _boxCollider2D.size.y) , 0f, Vector3.down, _checkLenght,
            _groundLayerMask);
    }
}