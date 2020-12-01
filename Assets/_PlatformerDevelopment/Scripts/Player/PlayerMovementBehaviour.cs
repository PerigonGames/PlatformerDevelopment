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
        private PlayerAnimationBehaviour _animation = null;

        // Movement 
        private float _movementSpeed = 5f;
        [SerializeField] private float _coolDownTimer = 1.0f;

        private int _axisSingle = 0;
        private float _hurtCoolDown = 0;

        private bool CanMove()
        {
            return _hurtCoolDown <= 0 && !_animation.IsAttacking();
        }

        public void Initialize(Rigidbody rigidbody, float movementSpeed, PlayerAnimationBehaviour animation = null)
        {
            _animation = animation;
            _movementSpeed = movementSpeed;
            _rigidbody = rigidbody;
        }
        
        public void OnPlayerHurt()
        {
            ResetCoolDown();
        }

        private void ResetCoolDown()
        {
            _hurtCoolDown = _coolDownTimer;
        }

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

        private void HurtCoolDownTimer()
        {
            if (_hurtCoolDown > 0)
            {
                _hurtCoolDown -= Time.fixedDeltaTime;
            }
        }
        
        #region mono

        private void FixedUpdate()
        {
            HurtCoolDownTimer();
            if (CanMove())
            {
                Movement();
                RotateBodyIfNeeded();
            }
        }

        #endregion
        
        #region Movement

        private void Movement()
        {
            var movement = new Vector3(_axisSingle, 0 ,0) * _movementSpeed * Time.fixedDeltaTime;
            _rigidbody.MovePosition(transform.position + movement);
        }

        private void RotateBodyIfNeeded()
        {
            if (_axisSingle == 1)
            {
                transform.rotation = Quaternion.identity;
            }
            else if (_axisSingle == -1)
            {
                transform.rotation = Quaternion.Euler(0, 180, 0);
            }
        }
        
        private void OnMovePerformed(InputAction.CallbackContext context)
        {
            _axisSingle = (int) context.ReadValue<Single>();
            DoAnimateRunning(true);
        }

        private void OnMoveCancelled()
        {
            _axisSingle = 0;
            DoAnimateRunning(false);
        }
        #endregion
        
        #region Animation

        private void DoAnimateRunning(bool isRunning)
        {
            if (_animation)
            {
                _animation.SetRunParameter(isRunning);
            }
        }
        #endregion
       
    }
}