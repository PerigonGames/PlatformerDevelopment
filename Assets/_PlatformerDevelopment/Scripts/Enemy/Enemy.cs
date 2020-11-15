using UnityEngine;

namespace PersonalDevelopment
{
    public enum EnemyState
    {
        Patrol,
        Attack
    }
    
    [RequireComponent(typeof(BoxCollider))]
    public abstract class Enemy : MonoBehaviour
    {
        [SerializeField] protected BoxCollider _detectionCollider = null;
        
        protected EnemyState _state = EnemyState.Patrol;
        protected bool _canAttack = false;
        protected GameObject _player;

        protected virtual void Setup()
        {
            _state = EnemyState.Patrol;
        }

        protected abstract void Attack();

        protected virtual void Patrol()
        {
            Debug.Log("Patrolling");
        }
        
        #if UNITY_EDITOR
        private void OnDrawGizmos()
        {
            Gizmos.color = _state == EnemyState.Attack ? Color.red : Color.blue;
            Gizmos.DrawWireCube(transform.position, _detectionCollider.size);

            if (_canAttack && _player != null)
            {
                Gizmos.color = Color.green;
                Gizmos.DrawLine(transform.position, _player.transform.position);
            }
        }
        #endif
        

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

        private void Awake()
        {
            Setup();
        }

        private void Update()
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
    }
}
