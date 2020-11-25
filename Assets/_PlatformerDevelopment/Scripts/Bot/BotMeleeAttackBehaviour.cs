using UnityEngine;

namespace PersonalDevelopment
{
    public class BotMeleeAttackBehaviour : MonoBehaviour
    {
        private IEnemyProperties _properties = null;
        
        public void Initialize(EnemyProperties properties)
        {
            _properties = properties;
        }
        
        #region Mono
        private void OnCollisionEnter(Collision other)
        {
            var collidedObject = other.gameObject;
            if (collidedObject.CompareTag("Player"))
            {
                var rigidBody = collidedObject.GetComponent<Rigidbody>();
                if (rigidBody)
                {
                    BumpPlayerBack(rigidBody);
                }

                var playerBehaviour = collidedObject.GetComponent<PlayerBehaviour>();
                if (playerBehaviour)
                {
                    DamagePlayerIfNeeded(playerBehaviour);
                }
            }
        }
        #endregion

        private void BumpPlayerBack(Rigidbody playerBody)
        {
            var direction = IsLeftHandSide(playerBody.gameObject) ? Vector3.left : Vector3.right;
            playerBody.velocity = Vector3.zero;
            playerBody.angularVelocity = Vector3.zero;
            playerBody.AddForce(direction * _properties.PushBackHorizontal(), ForceMode.Impulse);
            playerBody.AddForce(Vector3.up * _properties.PushBackVertical(), ForceMode.Impulse);
        }

        private void DamagePlayerIfNeeded(PlayerBehaviour player)
        {
            player.HurtPlayer();
        }

        private bool IsLeftHandSide(GameObject player)
        {
            return player.transform.position.x < transform.position.x;
        }
    }
}