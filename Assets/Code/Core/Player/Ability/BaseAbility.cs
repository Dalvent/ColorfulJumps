using UnityEngine;

namespace Code.Ability
{
    public class BaseAbility : MonoBehaviour
    {
        public BaseCharacter Character { get; private set; }
        
        public void Init(BaseCharacter character)
        {
            Character = character;
        }
    }
}