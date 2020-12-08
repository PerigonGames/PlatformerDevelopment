using UnityEngine;

namespace PersonalDevelopment
{
    public enum EnemyState
    {
        Patrol,
        Attack,
        Death
    }
    
    [RequireComponent(typeof(Rigidbody))]
    public abstract class Enemy : MonoBehaviour
    {
        private BaseCharacter _character = null;

        [SerializeField] private CollectibleBehaviour _collectible = null;
        // Components
        protected Rigidbody _rigidBody = null;

        //Properties
        protected EnemyState _state = EnemyState.Patrol;

        [SerializeField] protected EnemyProperties _properties = null;

        public void HurtBot()
        {
            _character.HitCharacter();
        }
        
        protected abstract void Patrol();
        protected abstract void Attack();
        protected abstract void Death();

        protected virtual void OnDeath()
        {
            _state = EnemyState.Death;
            _rigidBody.isKinematic = true;
            _rigidBody.detectCollisions = false;
            Instantiate(_collectible.gameObject, transform.position, Quaternion.identity);
        }

        #region Mono

        protected virtual void Awake()
        {
            _character = new BaseCharacter(_properties);
            _rigidBody = GetComponent<Rigidbody>();
            _state = EnemyState.Patrol;
        }

        protected virtual void OnEnable()
        {
            _character.OnDeath += OnDeath;
        }

        protected virtual void OnDisable()
        {
            _character.OnDeath -= OnDeath;
        }

        private void FixedUpdate()
        {
            switch (_state)
            {
                case EnemyState.Patrol:
                    Patrol();
                    break;
                case EnemyState.Attack:
                    Attack();
                    break;
                case EnemyState.Death:
                    Death();
                    break;
            }
        }

        #endregion
        
    }
}
