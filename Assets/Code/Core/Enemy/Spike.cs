using System;
using Code;
using UnityEngine;

public class Spike : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        var player = other.gameObject.GetComponent<Player>();
        if(player == null)
            return;

        player.Kill(KillerType.Spike);
    }
}
