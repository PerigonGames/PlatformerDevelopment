using System;
using UnityEngine;

namespace PersonalDevelopment
{
    public class BotRangedAttackBehaviour : MonoBehaviour
    {
        public event Action OnPlayerEnteredRange;
        public event Action OnPlayerExitRange;

        [SerializeField] private ProjectileBehaviour _projectile;
        private float _coolDown = 0f;

        private IEnemyProperties _properties;
        private BoxCollider _detectionCollider = null;
        private bool _canAttack = false;
        private GameObject _playerTarget = null;
        private bool _isGizmoTriggerEntered = false;

        public void Initialize(BoxCollider collider, float attackDetectionRange, IEnemyProperties properties)
        {
            _properties = properties;
            _detectionCollider = collider;
            _detectionCollider.size = new Vector3(attackDetectionRange, 1, 1);
        }
        
        public void Attack()
        {
            _coolDown -= Time.fixedDeltaTime;
            if (_coolDown < 0)
            {
                _coolDown = _properties.RangedAttackCoolDown();
                ShootProjectile();
            }
        }

        public void DisableRangedAttack()
        {
            _detectionCollider.enabled = false;
        }

        private void ShootProjectile()
        {
            if (_playerTarget != null)
            {
                var projectile = Instantiate(_projectile.gameObject);
                projectile.GetComponent<ProjectileBehaviour>().Initialize(IsLeftHandSide(), transform.position);
            }
        }

        private bool IsLeftHandSide()
        {
            return _playerTarget.transform.position.x < transform.position.x;
        }
        
        #region PlayerDetection
        
        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                if (OnPlayerEnteredRange != null)
                {
                    OnPlayerEnteredRange();
                }
                _playerTarget = other.gameObject;
                _isGizmoTriggerEntered = true;
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                if (OnPlayerExitRange != null)
                {
                    OnPlayerExitRange();
                }
                _canAttack = false;
                _playerTarget = null;
                _isGizmoTriggerEntered = false;
            }
        }

        #endregion
        
#if UNITY_EDITOR
        private void OnDrawGizmos()
        {
            if (_detectionCollider != null)
            {
                Gizmos.color = _isGizmoTriggerEntered ? Color.red : Color.blue;
                Gizmos.DrawWireCube(transform.position, _detectionCollider.size);
            }
            
            // Draw a line to player if enemy can detect them and is not blocked by another collider
            if (_canAttack && _playerTarget != null)
            {
                Gizmos.color = Color.green;
                Gizmos.DrawLine(transform.position, _playerTarget.transform.position);
            }
        }
#endif
    }
}

