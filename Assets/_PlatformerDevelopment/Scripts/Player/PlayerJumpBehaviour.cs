using UnityEngine;
using UnityEngine.InputSystem;

namespace PersonalDevelopment
{
    [RequireComponent(typeof(PlayerInput))]
    [RequireComponent(typeof(Rigidbody))]
    public class PlayerJumpBehaviour : MonoBehaviour
    {
        private const string FloorTag = "Floor";
        
        // Components
        private Rigidbody _rigidbody = null;
        private PlayerAnimationBehaviour _animator = null;
        
        // Fields
        [SerializeField] private float _jumpForce = 10f;
        private bool _canJump = true;

        #region PlayerInputInspector
        /// <summary>
        /// Used in PlayerInput Inspector
        /// </summary>
        /// <param name="context"></param>
        public void OnJumpPressed(InputAction.CallbackContext context)
        {
            if (!_animator.IsAttacking() && _canJump && context.phase == InputActionPhase.Performed)
            {
                _canJump = false;
                _rigidbody.AddForce(Vector3.up * _jumpForce, ForceMode.Impulse);
                _animator.SetJumpParameter(true);
            }
        }

        #endregion

        public void Initialize(Rigidbody rigidbody, PlayerAnimationBehaviour animator)
        {
            _animator = animator;
            _rigidbody = rigidbody;
        }
        
        public void OnPlayerHurt()
        {
            _canJump = false;
        }

        #region Mono
        private void OnCollisionEnter(Collision other)
        {
            if (other.gameObject.CompareTag(FloorTag))
            {
                _canJump = true;
                _animator.SetJumpParameter(false);
            }
        }
        #endregion


    }
}
