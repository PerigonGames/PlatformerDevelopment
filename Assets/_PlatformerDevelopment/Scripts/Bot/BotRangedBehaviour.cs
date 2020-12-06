using UnityEngine;

namespace PersonalDevelopment
{
    [RequireComponent(typeof(BoxCollider))]
    public class BotRangedBehaviour : Enemy
    {
        private BoxCollider _detectionCollider = null;
        private BotMovementBehaviour _botMovement = null;
        private BotRangedAttackBehaviour _botRangedAttack = null;
        private BotMeleeAttackBehaviour _botMeleeAttack = null;
        
        [SerializeField] private bool _moveLeftFirst = false;
        [SerializeField] private float _rangedDetection = 3f;

        protected override void Patrol()
        {
            _botMovement.MovementUpdate();
        }

        protected override void Attack()
        {
            _botRangedAttack.Attack();
        }

        protected override void Death()
        {
            // Nothing to do when on death state per FixedUpdate
        }

        protected override void OnDeath()
        {
            base.OnDeath();
            _botRangedAttack.DisableRangedAttack();
        }

        #region Mono
        protected override void Awake()
        {
            base.Awake();
            _detectionCollider = GetComponent<BoxCollider>();
            _botMovement = GetComponent<BotMovementBehaviour>();
            _botRangedAttack = GetComponent<BotRangedAttackBehaviour>();
            _botMeleeAttack = GetComponent<BotMeleeAttackBehaviour>();
        }

        private void Start()
        {
            _botRangedAttack.Initialize(_detectionCollider, _rangedDetection);
            _botMovement.Initialize(_rigidBody, _properties, _moveLeftFirst);
            _botMeleeAttack.Initialize(_properties);
        }

        protected override void OnEnable()
        {
            base.OnEnable();
            if (_botRangedAttack)
            {
                _botRangedAttack.OnPlayerEnteredRange += OnPlayerEnteredRange;
                _botRangedAttack.OnPlayerExitRange += OnPlayerExitRange;
            }
        }

        protected virtual void OnDisable()
        {
            base.OnDisable();
            _botRangedAttack.OnPlayerEnteredRange -= OnPlayerEnteredRange;
            _botRangedAttack.OnPlayerExitRange -= OnPlayerExitRange;
        }

        #endregion
        
        #region Delegate
        private void OnPlayerEnteredRange()
        {
            _state = EnemyState.Attack;
        }

        private void OnPlayerExitRange()
        {
            _state = EnemyState.Patrol;
        }
        
        #endregion
    }
}