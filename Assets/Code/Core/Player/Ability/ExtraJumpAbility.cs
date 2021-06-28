using System;
using UnityEngine;
using UnityEngine.Events;

namespace Code.Ability
{
    public class ExtraJumpAbility : BaseAbility
    {
        [SerializeField] private float _jumpForce = 1;
        [SerializeField] private int _maxExtraJumpCount = 1;
        
        [SerializeField] private UnityEvent _onJump;
        [SerializeField] private UnityEvent _onStopGainAltitude;

        private int _currentExtraJumpCount;
        private bool _gainingAltitude;

        private void Start()
        {
            UpdateExtraJumps();
        }

        private void Update()
        {
            if (_gainingAltitude && Character.Rigidbody2D.velocity.y <= 0)
            {
                _onStopGainAltitude.Invoke();
                _gainingAltitude = false;
            }
        }

        public void Jump()
        {
            if(HaveExtraJump())
                UseJump();
        }

        private bool HaveExtraJump()
        {
            return _currentExtraJumpCount > 0;
        }
        
        public void UpdateExtraJumps()
        {
            _currentExtraJumpCount = _maxExtraJumpCount;
        }

        private void UseJump()
        {
            Character.Rigidbody2D.velocity = new Vector2(Character.Rigidbody2D.velocity.x, _jumpForce);
            _currentExtraJumpCount--;
            _gainingAltitude = true;
            _onJump.Invoke();
        }
    }
}