using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PixelPerfectPositionFloor : MonoBehaviour
{
    [SerializeField] private int pixelPerUnit = 16;
    void FixedUpdate()
    {
        var newPosition = new Vector2
        {
            x = Mathf.Floor(transform.position.x),
            y = Mathf.Floor(transform.position.y)
        };
        transform.position = newPosition;
    }
}
