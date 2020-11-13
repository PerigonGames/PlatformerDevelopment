using UnityEngine;

namespace PersonalDevelopment
{
    [RequireComponent(typeof(Rigidbody))]
    public class BotMovementBehaviour : MonoBehaviour
    {
        // Components
        private Rigidbody _rigidbody = null;
        
        // Properties
        [SerializeField] private float _moveDistance = 5f;
        [SerializeField] private float _moveSpeed = 5f;
        [SerializeField] private bool _willMoveLeftFirst = false;

        private BotMovement _botMovement = null;
        
        #region Mono
        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody>();
        }

        private void OnEnable()
        {
            _botMovement = new BotMovement(transform.position, _moveDistance, _willMoveLeftFirst, Time.fixedDeltaTime);
        }

        private void OnDisable()
        {
            _botMovement = null;
        }

        private void FixedUpdate()
        {
            var destination = _botMovement.GetDestination(transform.position, _moveSpeed);
            _rigidbody.MovePosition(destination);
            _botMovement.UpdateDestinationIfNeeded(transform.position);
        }
        #endregion
        
    }
}