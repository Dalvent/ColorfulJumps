using System;
using System.Collections.Generic;
using Code.Ability;
using UnityEngine;

public abstract class BaseCharacter : MonoBehaviour
{
    public abstract bool IsOnGround { get; }
    public abstract Rigidbody2D Rigidbody2D { get; }
}