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

        public void SetPlayerModel()
        {
            DisablePlayerModels();
            _playerModels[Input.playerIndex].SetActive(true);
        }

        public void SetupPlayerForGame()
        {
            //TODO - Set player position
            //TODO - Add Player Jump Behaviour
            
            _movementBehaviour.Initialize(_rigidbody, _movementSpeed);
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

        private void DisablePlayerModels()
        {
            foreach (var model in _playerModels)
            {
                model.SetActive(false);
            }
        }
    }
}