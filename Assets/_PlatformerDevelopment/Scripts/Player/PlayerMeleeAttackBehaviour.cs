using UnityEngine;
using UnityEngine.InputSystem;

namespace PersonalDevelopment 
{
    public class PlayerMeleeAttackBehaviour : MonoBehaviour
    {
        [SerializeField] private float _meleeDistance = 3f;
        private PlayerAnimationBehaviour _animation = null;
        private GameObject _playerModel = null;
        [SerializeField] private float _pushBackHorizontal = 100f;
        
        //Fields
        private bool _canMelee = false;

        public void Initialize(PlayerAnimationBehaviour playerAnimation, GameObject playerModel)
        {
            _animation = playerAnimation;
            _playerModel = playerModel;
        }
        
        #region PlayerInputInspector

        /// <summary>
        /// Used in PlayerInput Inspector
        /// </summary>
        /// <param name="context"></param>
        public void OnMeleePressed(InputAction.CallbackContext context)
        {
            if (context.phase == InputActionPhase.Performed)
            {
                _canMelee = false;
                _animation.DoMeleeAttack();
                MeleeAttack();
            }
        }
        #endregion

        private void MeleeAttack()
        {
            var layerMask = 1 << LayerMask.NameToLayer("Bot");
            RaycastHit hit;
            // correct position, but the Vector3.up to shift the origin higher
            var originPosition = _playerModel.transform.position + Vector3.up;
            var directionToCast = _playerModel.transform.TransformDirection(Vector3.forward) * _meleeDistance;
            if (Physics.Raycast(originPosition, directionToCast, out hit, _meleeDistance, layerMask))
            {
                BumpPlayerBack(hit.rigidbody);
                DamageBot(hit.transform.gameObject.GetComponent<Enemy>());
            }
            
            Debug.DrawRay(originPosition + Vector3.up, _playerModel.transform.TransformDirection(Vector3.forward) * _meleeDistance, Color.blue, 3f);
        }

        private void DamageBot(Enemy bot)
        {
            bot.HurtBot();
        }

        private void BumpPlayerBack(Rigidbody playerBody)
        {
            var direction = IsLeftHandSide(playerBody.gameObject) ? Vector3.left : Vector3.right;
            playerBody.velocity = Vector3.zero;
            playerBody.angularVelocity = Vector3.zero;
            playerBody.AddForce(direction * _pushBackHorizontal, ForceMode.Impulse);
        }
        
        private bool IsLeftHandSide(GameObject bot)
        {
            return bot.transform.position.x < transform.position.x;
        }
    }
}