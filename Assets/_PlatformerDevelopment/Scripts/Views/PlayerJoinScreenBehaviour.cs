using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

namespace PersonalDevelopment
{
    public class PlayerJoinScreenBehaviour : MonoBehaviour
    {
        private const string KeyboardAndMouseWASD = "Keyboard&Mouse_WASD";
        private const string KeyboardAndMouseKeys = "Keyboard&Mouse_Keys";
        private const string InputMappingNameUI = "UI";
        private const string KeyboardSubmitKeys = "enter";
        private const string KeyboardSubmitWASD = "space";
        
        private PlayerInputManager _inputManager = null;
        [SerializeField] private PlayerSelectionBehaviour _playerOneSelection = null;
        [SerializeField] private PlayerSelectionBehaviour _playerTwoSelection = null;

        private PlayerCharacterBindings _characterBinding = null;

        private bool isWASDJoined = false;
        private bool isKeysJoined = false;
        
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

        private void OnEnable()
        {
            _characterBinding = new PlayerCharacterBindings();
            _characterBinding.UI.Submit.performed += OnSubmitPerformed;
            _characterBinding.Enable();
            InputManager.EnableJoining();
            InputManager.onPlayerJoined += OnPlayerJoined;
        }


        private void OnDisable()
        {
            _characterBinding.UI.Submit.performed -= OnSubmitPerformed;
            _characterBinding.Disable();
            _characterBinding = null;
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

        
        private void OnSubmitPerformed(InputAction.CallbackContext obj)
        {
            if (_inputManager.playerCount >= 2)
            {
                _inputManager.DisableJoining();
                return;
            }

            PlayerInput playerInput = null;
            var lowercase = obj.control.name.ToLower();
            switch (lowercase)
            {
                case KeyboardSubmitKeys when !isKeysJoined:
                    playerInput = InputManager.JoinPlayer();
                    playerInput.SwitchCurrentControlScheme(KeyboardAndMouseKeys, Keyboard.current);
                    isKeysJoined = true;
                    break;
                case KeyboardSubmitWASD when !isWASDJoined:
                    playerInput = InputManager.JoinPlayer();
                    playerInput.SwitchCurrentControlScheme(KeyboardAndMouseWASD, Keyboard.current);
                    isWASDJoined = true;
                    break;
                default:
                    InputManager.JoinPlayerFromActionIfNotAlreadyJoined(obj);
                    break;
            }
            
            var allPlayers = PlayerInput.all;
            foreach (var player in allPlayers)
            {
                player.SwitchCurrentActionMap(InputMappingNameUI);
            }
            
            StartCoroutine(PlayerInputBugFixer(playerInput));
        }

        //InputManager has some stupid bug that disabled inputs unless I turned it off and on.
        //https://stackoverflow.com/questions/63540724/errors-after-deleting-an-action-using-unitys-new-input-system
        private IEnumerator PlayerInputBugFixer(PlayerInput input)
        {
            if (input != null)
            {
                input.gameObject.SetActive(false);
                yield return new WaitForEndOfFrame();
                input.gameObject.SetActive(true);
            }

            yield return null;
        }
        #endregion
    }
}

