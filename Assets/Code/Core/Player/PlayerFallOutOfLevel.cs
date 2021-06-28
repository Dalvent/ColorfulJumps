using UnityEngine;

namespace Code
{
    [RequireComponent(typeof(Player))]
    public class PlayerFallOutOfLevel : MonoBehaviour
    {
        [SerializeField]
        public float _killY;

        private Player _player;

        private void Start()
        {
            _player = GetComponent<Player>();
        }

        void Update()
        {
            if (gameObject.transform.position.y < _killY)
            {
                if(_player.IsDead)
                    return;
                
                _player.Kill(KillerType.FallOutOfLevel);
            }
        }
    }
}