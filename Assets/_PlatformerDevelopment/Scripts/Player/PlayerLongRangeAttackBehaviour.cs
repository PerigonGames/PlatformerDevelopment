using UnityEngine;
using UnityEngine.InputSystem;

namespace PersonalDevelopment
{
    public class PlayerLongRangeAttackBehaviour : MonoBehaviour
    {
        // Dependencies
        private PlayerAnimationBehaviour _animation = null;
        private GameObject _playerModel = null;
        
        //Properties
        private bool _canShoot = false;
        
        public void Initialize(PlayerAnimationBehaviour playerAnimation, GameObject playerModel)
        {
            _animation = playerAnimation;
            _playerModel = playerModel;
        }
        
        #region PlayerInputInspector

        public void OnLongRangePressed(InputAction.CallbackContext context)
        {
            if (context.phase == InputActionPhase.Performed)
            {
                _canShoot = false;
                _animation.DoShootAttack();
                ShootProjectile();
            }
        }
        #endregion

        private void ShootProjectile()
        {
            Debug.Log("Shoot");
        }
    }
}