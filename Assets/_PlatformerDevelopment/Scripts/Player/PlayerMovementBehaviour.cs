using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace PersonalDevelopment
{
    [RequireComponent(typeof(PlayerInput))]
    [RequireComponent(typeof(Rigidbody))]
    public class PlayerMovementBehaviour : MonoBehaviour
    {
        // Components
        private Rigidbody _rigidbody = null;
        
        // Movement 
        [SerializeField] private float _movementSpeed = 5f;
        private int _axisSingle = 0;

        #region PlayerInputInspector

        /// <summary>
        /// Used in inspector for player Input
        /// </summary>
        /// <param name="context"></param>
        public void OnMove(InputAction.CallbackContext context)
        {
            switch (context.phase)
            {
                case InputActionPhase.Performed:
                    OnMovePerformed(context);
                    break;
                case InputActionPhase.Canceled:
                    OnMoveCancelled();
                    break;
            }
        }

        #endregion
        
        #region mono
        // Start is called before the first frame update
        private void Awake()
        {
            // This will never be null, since this won't even compile without RigidBody on this gameobject
            _rigidbody = GetComponent<Rigidbody>();
        }
        
        private void FixedUpdate()
        {
            Movement();
        }

        #endregion
        
        #region Movement

        private void Movement()
        {
            var movement = new Vector3(_axisSingle, 0 ,0) * _movementSpeed * Time.fixedDeltaTime;
            _rigidbody.MovePosition(transform.position + movement);
        }
        
        private void OnMovePerformed(InputAction.CallbackContext context)
        {
            _axisSingle = (int) context.ReadValue<Single>();
        }

        private void OnMoveCancelled()
        {
            _axisSingle = 0;
        }
        #endregion
    }
}