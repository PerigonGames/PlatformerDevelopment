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

        [SerializeField] private List<GameObject> _playerModels = null;

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
            var playerModel = _playerModels[Input.playerIndex];
            playerModel.SetActive(true);
            _animationBehaviour.Initialize(playerModel.GetComponent<Animator>());
            SetComponent(false);
            transform.rotation = Quaternion.Euler(0, 90, 0);
            _animationBehaviour.SetTauntParameter(true);
        }

        public void SetupPlayerForGame()
        {
            //TODO - Set player position
            //TODO - Initialize Player Jump Behaviour
            
            _movementBehaviour.Initialize(_rigidbody, _movementSpeed, _animationBehaviour);
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