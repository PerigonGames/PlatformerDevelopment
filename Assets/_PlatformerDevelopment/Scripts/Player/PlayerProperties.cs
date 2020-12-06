using Sirenix.OdinInspector;
using UnityEngine;

namespace PersonalDevelopment
{
    [CreateAssetMenu(fileName = "Player Property", menuName = "PersonalDevelopment/PlayerProperties", order = 2)]
    public class PlayerProperties : ScriptableObject, IPlayerProperties
    {
        [Title("Movement Properties")] 
        [SerializeField] private float _moveSpeed = 0;
        [SerializeField] private float _jumpForce = 0;

        [Title("Character Properties")] 
        [SerializeField] private int _health = 0;
        
        public float MoveSpeed()
        {
            return _moveSpeed;
        }

        public float JumpForce()
        {
            return _jumpForce;
        }

        public int Health()
        {
            return _health;
        }
    }
}