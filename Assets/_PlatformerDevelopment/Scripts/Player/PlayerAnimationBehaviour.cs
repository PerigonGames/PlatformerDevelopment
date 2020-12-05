
using UnityEngine;

namespace PersonalDevelopment
{
    public class PlayerAnimationBehaviour : MonoBehaviour
    {
        private const string IsRunningAnimationParameter = "IsRunning";
        private const string IsTauntingAnimationParameter = "IsTaunting";
        private const string IsJumpingAnimationParameter = "IsJumping";
        private const string DoMeleeParameter = "DoMelee";
        private const string DoShootParameter = "DoShoot";
        private const string HurtParameter = "Hurt";
        private const string DeathParameter = "IsDead";

        private Animator _animator = null;

        public void Initialize(Animator animator)
        {
            _animator = animator;
        }

        
        #region Animation
        public void SetRunParameter(bool isRunning)
        {
            if (_animator)
            {
                _animator.SetBool(IsRunningAnimationParameter, isRunning);
            }
        }

        public void SetJumpParameter(bool isJumping)
        {
            if (_animator)
            {
                _animator.SetBool(IsJumpingAnimationParameter, isJumping);
            }
        }

        public void SetTauntParameter(bool isTaunting)
        {
            if (_animator)
            {
                _animator.SetBool(IsTauntingAnimationParameter, isTaunting);
            }
        }

        public void DoMeleeAttack()
        {
            if (_animator)
            {
                _animator.SetTrigger(DoMeleeParameter);
            }
        }

        public void DoShootAttack()
        {
            if (_animator)
            {
                _animator.SetTrigger(DoShootParameter);
            }
        }

        public void Hurt()
        {
            if (_animator && !_animator.GetCurrentAnimatorStateInfo(0).IsName("Hurt"))
            {
                _animator.SetTrigger(HurtParameter);
            }
        }

        public void SetDead(bool isDead)
        {
            if (_animator)
            {
                _animator.SetBool(DeathParameter, isDead);
            }
        }
        
        #endregion
        
        #region Helpers

        public bool IsKicking()
        {
            if (_animator)
            {
                return _animator.GetCurrentAnimatorStateInfo(0).IsName("Melee");
            }

            return false;
        }

        public bool IsShooting()
        {
            if (_animator)
            {
                return _animator.GetCurrentAnimatorStateInfo(0).IsName("Shoot");
            }

            return false;
        }

        public bool IsAttacking()
        {
            return IsShooting() || IsKicking();
        }
        
        #endregion
    }

}