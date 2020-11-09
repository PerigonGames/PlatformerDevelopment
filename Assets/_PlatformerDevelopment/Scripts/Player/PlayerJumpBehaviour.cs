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
        private PlayerCharacterBindings _bindings = null;
        private Rigidbody _rigidbody = null;
        
        // Fields
        [SerializeField] private float _jumpForce = 10f;
        private bool _canJump = true;

        #region Movement

        private void OnJumpPressed(InputAction.CallbackContext context)
        {
            if (_canJump)
            {
                _canJump = false;
                _rigidbody.AddForce(Vector3.up * _jumpForce, ForceMode.Impulse);
            }
        }

        #endregion
        
        #region Mono
        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody>();
            _bindings = new PlayerCharacterBindings();
        }

        private void OnEnable()
        {
            _bindings.Player.Jump.performed += OnJumpPressed;
            _bindings.Enable();
        }

        private void OnDisable()
        {
            _bindings.Player.Jump.performed -= OnJumpPressed;
            _bindings.Disable();
        }

        private void OnCollisionEnter(Collision other)
        {
            if (other.gameObject.CompareTag(FloorTag))
            {
                _canJump = true;
            }
        }

        #endregion
    }
}
