using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;

[RequireComponent(typeof(SpriteRenderer))]
public class OutOfMapArrow : MonoBehaviour
{
    [SerializeField]
    private GameObject _target;
    private SpriteRenderer _spriteRenderer;
    private Camera _mainCamera;

    [SerializeField] private TMP_Text _tmpText;
    [SerializeField] private float _metersPerUnityPosition;

    private void Start()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _mainCamera = Camera.main;
    }

    public void Update()
    {
        var cameraSize = _mainCamera.orthographicSize;
        var showArrow = cameraSize + 1f <= _target.transform.position.y;
        _spriteRenderer.enabled = showArrow;
        _tmpText.enabled = showArrow;
        if (showArrow)
        {
            var targetPosition = _target.transform.position;
            transform.position = new Vector3(targetPosition.x, cameraSize - 1f);
            _tmpText.text = ((targetPosition.y - cameraSize) / _metersPerUnityPosition).ToString("F1", CultureInfo.InvariantCulture) + " m."; ;
        }
    }
}
