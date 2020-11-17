using UnityEngine;
using UnityEngine.InputSystem;

namespace PersonalDevelopment
{
    [RequireComponent(typeof(PlayerInputManager))]
    public class PlayerJoinScreenBehaviour : MonoBehaviour
    {

        private PlayerInputManager _inputManager = null;
        
        private void Awake()
        {
            _inputManager = GetComponent<PlayerInputManager>();
        }

        private void OnEnable()
        {
            _inputManager.onPlayerJoined += OnPlayerJoined;
        }

        private void OnDisable()
        {
            _inputManager.onPlayerJoined -= OnPlayerJoined;
        }


        #region Delegate
        private void OnPlayerJoined(PlayerInput input)
        {
            
        }
        #endregion
    }
}

