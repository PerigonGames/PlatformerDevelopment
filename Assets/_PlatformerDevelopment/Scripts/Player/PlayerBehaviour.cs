using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace PersonalDevelopment
{
    [RequireComponent(typeof(Rigidbody))]
    public class PlayerBehaviour : MonoBehaviour
    {
        private enum PlayerState
        {
            Menu,
            Game
        }
        
        private Rigidbody _rigidbody = null;
        private PlayerInput _playerInput = null;

        [SerializeField] private GameObject[] _playerModels = null;
        
        private PlayerState _state = PlayerState.Menu;

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
        
        private PlayerState State
        {
            get => _state;
            set
            {
                _state = value;
                OnStateSet();
            }
        }

        public void SetPlayerModel()
        {
            _playerModels[0].SetActive(false);
            _playerModels[1].SetActive(false);
            _playerModels[Input.playerIndex].SetActive(true);
        }


        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody>();
        }

        private void OnEnable()
        {
            State = PlayerState.Menu;
        }

        private void OnStateSet()
        {
            switch (State)
            {
                case PlayerState.Menu:
                    OnMenuStateSet();
                    break;
                case PlayerState.Game:
                    OnGameStateSet();
                    break;
            }
        }

        private void OnMenuStateSet()
        {
            _rigidbody.isKinematic = true;
        }

        private void OnGameStateSet()
        {
            _rigidbody.isKinematic = false;
        }

    }
}