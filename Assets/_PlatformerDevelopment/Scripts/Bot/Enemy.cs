using UnityEngine;

namespace PersonalDevelopment
{
    public enum EnemyState
    {
        Patrol,
        Attack
    }
    
    [RequireComponent(typeof(Rigidbody))]
    public abstract class Enemy : MonoBehaviour
    {
        // Components
        protected Rigidbody _rigidBody = null;

        //Properties
        protected EnemyState _state = EnemyState.Patrol;

        [SerializeField] protected EnemyProperties _properties = null;

        public void HurtBot()
        {
            //TODO - What happens when the bot gets hurt
        }
        
        protected abstract void Patrol();
        protected abstract void Attack();

        #region Mono

        protected virtual void Awake()
        {
            _rigidBody = GetComponent<Rigidbody>();
            _state = EnemyState.Patrol;
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
            }
        }

        #endregion
        
    }
}
