
using UnityEngine;

namespace PersonalDevelopment
{
    public class PlayerAnimationBehaviour : MonoBehaviour
    {
        private const string IsRunningAnimationParameter = "IsRunning";
        private const string IsTauntingAnimationParameter = "IsTaunting";
        private const string DoMeleeParameter = "DoMelee";

        private Animator _animator = null;

        public void Initialize(Animator animator)
        {
            _animator = animator;
        }

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
        
        public bool IsKicking()
        {
            if (_animator)
            {
                return _animator.GetCurrentAnimatorStateInfo(0).IsName("Melee");
            }

            return false;
        }
    }

}