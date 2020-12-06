using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using Quaternion = UnityEngine.Quaternion;
using Vector3 = UnityEngine.Vector3;

namespace PersonalDevelopment
{
    public class PlayerLongRangeAttackBehaviour : MonoBehaviour
    {
        // Dependencies
        private PlayerAnimationBehaviour _animation = null;
        [SerializeField] private ProjectileBehaviour _shotProjectile = null;

        //Properties
        private bool _canShoot = true;
        
        public void Initialize(PlayerAnimationBehaviour playerAnimation)
        {
            _animation = playerAnimation;
        }
        
        #region PlayerInputInspector

        public void OnLongRangePressed(InputAction.CallbackContext context)
        {
            if (context.phase == InputActionPhase.Performed)
            {
                _animation.DoShootAttack();
                ShootProjectileIfAble();
            }
        }
        #endregion

        private void ShootProjectileIfAble()
        {
            if (_canShoot)
            {
                StartCoroutine(DelayedProjectileShot());
                _canShoot = false;
            }
        }

        private IEnumerator DelayedProjectileShot()
        {
            // Time to wait for the Animation Duration
            yield return new WaitForSeconds(_animation.ShootAnimationTime() / 2);
            if (_animation.IsShooting())
            {
                ShootProjectile();
            }
            _canShoot = true;
        }

        private void ShootProjectile()
        {
            var projectile = Instantiate(_shotProjectile.gameObject);
            projectile.GetComponent<ProjectileBehaviour>().Initialize(!IsLookingRight(), transform.position);
        }

        private bool IsLookingRight()
        {
            return transform.rotation == Quaternion.identity;
        }
    }
}