using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private Player _target;
    [SerializeField] private bool _isInputEnabled = true;

    public Player Target => _target;
    public bool IsInputEnabled => _isInputEnabled; 

    public void Possess(Player target)
    {
        _target = target;
    }

    public void DisableInput() => _isInputEnabled = false;
    public void EnableInput() => _isInputEnabled = true;
    
    // Update is called once per frame
    void Update()
    {
        if (_isInputEnabled == false)
            return;
        
        _target.Move(new Vector2(Input.GetAxis("Horizontal"), 0));

        var jumpButtonPressed = Input.GetButtonDown("Jump");
        if(jumpButtonPressed)
            _target.Jump();
    }
}
