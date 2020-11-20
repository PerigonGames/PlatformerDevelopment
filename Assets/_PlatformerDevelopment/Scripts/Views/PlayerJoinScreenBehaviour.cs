using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace PersonalDevelopment
{
    public class PlayerJoinScreenBehaviour : MonoBehaviour
    {
        private const string KeyboardAndMouseWASD = "Keyboard&Mouse_WASD";
        private const string KeyboardAndMouseKeys = "Keyboard&Mouse_Keys";
        
        private PlayerInputManager _inputManager = null;
        [SerializeField] private PlayerSelectionBehaviour _playerOneSelection = null;
        [SerializeField] private PlayerSelectionBehaviour _playerTwoSelection = null;

        private PlayerCharacterBindings _characterBinding = null;

        private PlayerInputManager InputManager
        {
            get
            {
                if (_inputManager == null)
                {
                    _inputManager = PlayerInputManager.instance;
                }

                return _inputManager;
            }
        }
        
        private void Awake()
        {
            _characterBinding = new PlayerCharacterBindings();
            _characterBinding.UI.Submit.canceled += OnSubmitPerformed;
            _characterBinding.UI.Submit.performed += OnSubmitPerformed;
            _characterBinding.UI.Submit.started += OnSubmitPerformed;
            _characterBinding.Enable();
        }

        private void OnDestroy()
        {
            _characterBinding.UI.Submit.canceled -= OnSubmitPerformed;
            _characterBinding.UI.Submit.performed -= OnSubmitPerformed;
            _characterBinding.UI.Submit.started -= OnSubmitPerformed;
            _characterBinding.Disable();
            _characterBinding = null;
        }
        

        private void OnEnable()
        {
            InputManager.EnableJoining();
            InputManager.onPlayerJoined += OnPlayerJoined;
        }


        private void OnDisable()
        {
            InputManager.DisableJoining();
            InputManager.onPlayerJoined -= OnPlayerJoined;
        }


        #region Delegate

        private void OnPlayerJoined(PlayerInput input)
        {
            if (input.playerIndex == 0)
            {
                _playerOneSelection.Initialize(input);
            }
            else
            {
                _playerTwoSelection.Initialize(input);
            }
        }

        private bool isWASDJoined = false;
        private bool isKeysJoined = false;
        
        private void OnSubmitPerformed(InputAction.CallbackContext obj)
        {
            if (_inputManager.playerCount >= 2)
            {
                _inputManager.DisableJoining();
            }
            
            var lowercase = obj.control.name.ToLower();
            if (lowercase == "enter")
            {
                if (isKeysJoined) return;
                
                var playerInput = InputManager.JoinPlayer();
                playerInput.SwitchCurrentControlScheme(KeyboardAndMouseKeys, Keyboard.current);
                playerInput.SwitchCurrentActionMap("UI");
                isKeysJoined = true;
                return;
            }

            if (lowercase == "space")
            {
                if (isWASDJoined) return;
                
                var playerInput = InputManager.JoinPlayer();
                playerInput.SwitchCurrentControlScheme(KeyboardAndMouseWASD, Keyboard.current);
                playerInput.SwitchCurrentActionMap("UI");
                isWASDJoined = true;
                return;
            }

            var controllerInput = InputManager.JoinPlayer(pairWithDevice: obj.control.device);
            controllerInput.SwitchCurrentActionMap("UI");
        }
        #endregion
    }
}

