
using UnityEngine;

namespace PersonalDevelopment
{
    public class PlayerAnimationBehaviour : MonoBehaviour
    {
        private const string IsRunningAnimationParameter = "IsRunning";
        private const string IsTauntingAnimationParameter = "IsTaunting";

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
    }

}