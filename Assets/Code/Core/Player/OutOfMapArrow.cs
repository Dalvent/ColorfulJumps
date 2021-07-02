using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

[RequireComponent(typeof(SpriteRenderer))]
public class OutOfMapArrow : MonoBehaviour
{
    [SerializeField]
    private GameObject _target;
    private SpriteRenderer _spriteRenderer;

    [SerializeField]
    private float startTargetYCoordinatesToAppeared = 10f;

    private void Start()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void Update()
    {
        var showArrow = startTargetYCoordinatesToAppeared <= _target.gameObject.transform.position.y;
        _spriteRenderer.enabled = showArrow;
        if (showArrow)
        {
            transform.position = new Vector3(_target.transform.position.x, transform.position.y);
        }
    }
}
