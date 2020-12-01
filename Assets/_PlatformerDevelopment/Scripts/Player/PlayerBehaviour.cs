using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.InputSystem;

namespace PersonalDevelopment
{
    [RequireComponent(typeof(Rigidbody))]
    public class PlayerBehaviour : MonoBehaviour
    {
        private Rigidbody _rigidbody = null;
        private PlayerInput _playerInput = null;
        private PlayerMovementBehaviour _movementBehaviour = null;
        private PlayerJumpBehaviour _jumpBehaviour = null;
        private PlayerAnimationBehaviour _animationBehaviour = null;
        private PlayerMeleeAttackBehaviour _meleeAttackBehaviour = null;
        private PlayerLongRangeAttackBehaviour _longRangeAttackBehaviour = null;

        [SerializeField] private List<GameObject> _playerModels = null;
        private GameObject _chosenPlayerModel = null;

        [Title("Character Properties - Move to ScriptableObject")] [SerializeField]
        private float _movementSpeed = 8f;

        private PlayerInput Input
        {
            get
            {
                if (_playerInput == null)
                {
                    _playerInput = GetComponent<PlayerInput>();
                }

                return _playerInput;
            }
        }

        public void SetPlayerModelInCharacterSelection()
        {
            DisablePlayerModels();
            _chosenPlayerModel = _playerModels[Input.playerIndex];
            _chosenPlayerModel.SetActive(true);
            _animationBehaviour.Initialize(_chosenPlayerModel.GetComponent<Animator>());
            SetComponent(false);
            transform.rotation = Quaternion.Euler(0, 90, 0);
            _animationBehaviour.SetTauntParameter(true);
        }

        public void SetupPlayerForGame()
        {
            //TODO - Set player position
            
            _movementBehaviour.Initialize(_rigidbody, _movementSpeed, _animationBehaviour);
            _jumpBehaviour.Initialize(_rigidbody, _animationBehaviour);
            _meleeAttackBehaviour.Initialize(_animationBehaviour, _chosenPlayerModel);
            _longRangeAttackBehaviour.Initialize(_animationBehaviour, _chosenPlayerModel);
            _animationBehaviour.SetTauntParameter(false);
            SetComponent(true);
            Input.SwitchCurrentActionMap("Player");
            _rigidbody.isKinematic = false;
        }

        public void HurtPlayer()
        {
            _movementBehaviour.OnPlayerHurt();
            _jumpBehaviour.OnPlayerHurt();
        }

        private void Awake()
        {
            _longRangeAttackBehaviour = GetComponent<PlayerLongRangeAttackBehaviour>();
            _meleeAttackBehaviour = GetComponent<PlayerMeleeAttackBehaviour>();
            _movementBehaviour = GetComponent<PlayerMovementBehaviour>();
            _jumpBehaviour = GetComponent<PlayerJumpBehaviour>();
            _animationBehaviour = GetComponent<PlayerAnimationBehaviour>();
            _rigidbody = GetComponent<Rigidbody>();
        }

        private void OnEnable()
        {
            _rigidbody.isKinematic = true;
        }

        private void OnDisable()
        {
            DisablePlayerModels();
        }

        private void SetComponent(bool isEnabled)
        {
            if (_movementBehaviour != null)
            {
                _movementBehaviour.enabled = isEnabled;
            }

            if (_jumpBehaviour != null)
            {
                _jumpBehaviour.enabled = isEnabled;
            }
        }
        
        private void DisablePlayerModels()
        {
            foreach (var model in _playerModels)
            {
                model.SetActive(false);
            }
        }
    }
}