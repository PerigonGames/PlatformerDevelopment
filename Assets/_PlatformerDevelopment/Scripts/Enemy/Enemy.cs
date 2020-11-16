using System;
using UnityEngine;

namespace PersonalDevelopment
{
    public enum EnemyState
    {
        Patrol,
        Attack
    }
    
    [RequireComponent(typeof(BoxCollider))]
    [RequireComponent(typeof(Rigidbody))]
    public abstract class Enemy : MonoBehaviour
    {
        protected BoxCollider _detectionCollider = null;
        protected Rigidbody _rigidBody = null;
        
        protected EnemyState _state = EnemyState.Patrol;
        protected bool _canAttack = false;
        protected GameObject _player;

        [SerializeField] private float _moveDistance = 5f;
        [SerializeField] private float _moveSpeed = 5f;
        [SerializeField] private bool _willMoveLeftFirst = false;

        private BotMovement _botMovement = null;
        
        protected virtual void Setup()
        {
            _detectionCollider = GetComponent<BoxCollider>();
            _rigidBody = GetComponentInParent<Rigidbody>();
            _state = EnemyState.Patrol;
        }

        protected abstract void Attack();

        protected virtual void Patrol()
        {
            Vector3 destination = _botMovement.GetDestination(transform.position, _moveSpeed);
            _rigidBody.MovePosition(destination);
            _botMovement.UpdateDestinationIfNeeded(transform.position);
        }
        
        #if UNITY_EDITOR
        private void OnDrawGizmos()
        {
            if (_detectionCollider != null)
            {
                Gizmos.color = _state == EnemyState.Attack ? Color.red : Color.blue;
                Gizmos.DrawWireCube(transform.position, _detectionCollider.size);
            }

            if (_canAttack && _player != null)
            {
                Gizmos.color = Color.green;
                Gizmos.DrawLine(transform.position, _player.transform.position);
            }
        }
        #endif

        #region PlayerDetection
        
        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                _state = EnemyState.Attack;
                _player = other.gameObject;
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                _state = EnemyState.Patrol;
                _canAttack = false;
                _player = null;
            }
        }

        #endregion


        #region Mono

        private void Awake()
        {
            Setup();
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
            switch (_state)
            {
                case EnemyState.Patrol:
                {
                    Patrol();
                    break;
                }
                case EnemyState.Attack:
                {
                    Attack();
                    break;
                }
                default:
                {
                    Patrol();
                    break;
                }
            }
        }

        #endregion
        
    }
}
