using UnityEngine;

namespace PersonalDevelopment
{
    public class BotMovementBehaviour : MonoBehaviour
    {
        // Components
        private Rigidbody _rigidbody = null;
        
        // Properties
        private EnemyProperties _properties = null;
        private BotMovement _botMovement = null;

        public void Initialize(Rigidbody rigidbody, EnemyProperties properties)
        {
            _rigidbody = rigidbody;
            _properties = properties;            
            _botMovement = new BotMovement(transform.position, _properties.MoveDistance, _properties.WillMoveLeftFirst, Time.fixedDeltaTime);
        }
        
        public void MovementUpdate()
        {
            var destination = _botMovement.GetDestination(transform.position, _properties.MoveSpeed);
            _rigidbody.MovePosition(destination);
            _botMovement.UpdateDestinationIfNeeded(transform.position);
        }
        
        #region Mono
        private void OnDestroy()    
        {
            _botMovement = null;
        }
        #endregion
        
    }
}