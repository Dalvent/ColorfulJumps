using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeWall : MonoBehaviour
{
    [SerializeField]
    private float _speed = 0.05f;
    [SerializeField][Range(0, 1)]
    private float _playerSpeedDependencePersent = 0.05f;
    [SerializeField]
    private float _stopDistancePlayer = 5.0f;

    private void Update()
    {
        if(NeedToStop())
            return;
        
        transform.position += Vector3.right * ((SpikeWallSpeed() + CalculatePlayerSpeedDependence()) * Time.deltaTime);
    }

    private float SpikeWallSpeed()
    {
        return _speed * GameManager.Instance.SpeedMultiplayer;
    }

    private bool NeedToStop()
    {
        return transform.position.x - _stopDistancePlayer > GameManager.Instance.Player.transform.position.x;
    }
    
    private float CalculatePlayerSpeedDependence()
    {
        var result = (GameManager.Instance.Player.transform.position.x - transform.position.x) * _playerSpeedDependencePersent;
        return result > 0 ? result : 0;
    }
}
