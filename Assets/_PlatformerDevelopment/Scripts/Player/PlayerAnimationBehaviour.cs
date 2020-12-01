
using UnityEngine;

namespace PersonalDevelopment
{
    public class PlayerAnimationBehaviour : MonoBehaviour
    {
        private const string IsRunningAnimationParameter = "IsRunning";
        private const string IsTauntingAnimationParameter = "IsTaunting";
        private const string DoMeleeParameter = "DoMelee";
        private const string DoShootParameter = "DoShoot";

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
        
        #endregion
    }

}