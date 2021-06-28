using UnityEngine;
using UnityEngine.Events;

namespace Code.Ability
{
    public class MoveAbility : BaseAbility
    {
        [SerializeField] private UnityEvent _onStartMove;
        [SerializeField] private UnityEvent _onStopMove;
        
        [SerializeField] private float _speed;

        private bool _isMoving;

        public virtual void Move(Vector2 moveVector)
        {
            if (_isMoving && moveVector == Vector2.zero)
            {
                _onStopMove.Invoke();
                _isMoving = false;
            }
            else if (!_isMoving && moveVector != Vector2.zero)
            {
                _onStartMove.Invoke();
                _isMoving = true;
            }
            
            Character.Rigidbody2D.velocity = new Vector2(
                moveVector.x * _speed * GameManager.Instance.SpeedMultiplayer, 
                Character.Rigidbody2D.velocity.y
            );
        }
    }
}