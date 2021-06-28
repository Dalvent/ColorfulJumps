using System;
using System.Collections;
using Code.Core;
using UnityEngine;
using UnityEngine.Events;

namespace Code.Ability
{
    public class DashAbility : BaseAbility
    {
        [SerializeField] private UnityEvent _startDash;
        [SerializeField] private UnityEvent _endDash;
        
        [SerializeField] private float _dashSpeed = 1.0f;
        [SerializeField] private float _dashDistance = 1.0f;
        [SerializeField] private float _afterDashDelay = 1.0f;

        private float DashTime => _dashDistance / (_dashSpeed * GameManager.Instance.SpeedMultiplayer);
        
        public bool IsDashing => _isDashing;
        public bool CanDash => _canUseDash;
        
        private bool _isDashing;
        private bool _canUseDash = true;
        private Vector2 _dashDirection;
        

        public void StartDashing(Vector2 dashDirection)
        {
            if(!CanDash || IsDashing)
                return;

            _isDashing = true;
            _canUseDash = false;
            _dashDirection = dashDirection;
            StartCoroutine(StartDashProcessTimer());
        }
        public void MoveWithDash()
        {
            Character.Rigidbody2D.velocity = _dashDirection * (_dashSpeed * GameManager.Instance.SpeedMultiplayer);
        }
        
        private IEnumerator StartDashProcessTimer()
        {
            _startDash.Invoke();
            var gravityAtStart = Character.Rigidbody2D.gravityScale;
            Character.Rigidbody2D.gravityScale = 0;
            
            yield return new WaitForSeconds(DashTime);
            if (!Character.Rigidbody2D.IsSleeping())
            {
                Character.Rigidbody2D.gravityScale = gravityAtStart;
            }
            _isDashing = false;
            _endDash.Invoke();
            
            yield return new WaitForSeconds(_afterDashDelay);
            _canUseDash = true;
        }
    }
}