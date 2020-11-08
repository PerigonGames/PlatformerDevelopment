using UnityEngine;
using UnityEngine.InputSystem;

namespace PersonalDevelopment
{
    [RequireComponent(typeof(PlayerInput))]
    [RequireComponent(typeof(Rigidbody))]
    public class PlayerMovementBehaviour : MonoBehaviour
    {
        // Components
        private PlayerCharacterBindings _bindings = null;
        private Rigidbody _rigidbody = null;
        
        // Movement 
        [SerializeField] private float _movementSpeed = 5f;
        private Vector2 Axis2D { get; set; }

        #region mono
        
        // Start is called before the first frame update
        private void Awake()
        {
            // This will never be null, since this won't even compile without RigidBody on this gameobject
            _rigidbody = GetComponent<Rigidbody>();
            _bindings = new PlayerCharacterBindings();
        }
        
        private void OnEnable()
        {
            _bindings.Player.Move.performed += OnMovePressed;
            _bindings.Player.Move.canceled += OnMoveCancelled;
            _bindings.Enable();
        }

        private void OnDisable()
        {
            _bindings.Player.Move.performed -= OnMovePressed;
            _bindings.Player.Move.canceled -= OnMoveCancelled;
            _bindings.Disable();
        }

        private void FixedUpdate()
        {
            Movement();
        }

        #endregion
        
        #region Movement

        private void Movement()
        {
            var movement = new Vector3(Axis2D.x, 0 ,0) * _movementSpeed * Time.fixedDeltaTime;
            _rigidbody.MovePosition(transform.position + movement);
        }
        #endregion
        
        #region Delegate
        private void OnMovePressed(InputAction.CallbackContext context)
        {
            Axis2D = context.ReadValue<Vector2>();
        }

        private void OnMoveCancelled(InputAction.CallbackContext context)
        {
            Axis2D = Vector3.zero;
        }
        #endregion
    }
}