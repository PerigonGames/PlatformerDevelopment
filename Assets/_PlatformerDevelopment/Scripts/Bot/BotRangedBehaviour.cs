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

        protected override void Patrol()
        {
            _botMovement.MovementUpdate();
        }

        protected override void Attack()
        {
            _botRangedAttack.Attack();
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
            _botRangedAttack.Initialize(_detectionCollider);
            _botMovement.Initialize(_rigidBody, _properties, _moveLeftFirst);
            _botMeleeAttack.Initialize(_properties);
        }

        private void OnEnable()
        {
            if (_botRangedAttack)
            {
                _botRangedAttack.OnPlayerEnteredRange += OnPlayerEnteredRange;
                _botRangedAttack.OnPlayerExitRange += OnPlayerExitRange;
            }
        }

        private void OnDisable()
        {
            _botRangedAttack.OnPlayerEnteredRange -= OnPlayerEnteredRange;
            _botRangedAttack.OnPlayerExitRange += OnPlayerExitRange;
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