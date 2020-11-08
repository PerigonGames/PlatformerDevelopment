using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

namespace PersonalDevelopment
{
    public class PlayerJoinScreenBehaviour : MonoBehaviour
    {
        private Keyboard _keyboard = null;
        private void Awake()
        {
            _keyboard = Keyboard.current;
        }
        
        private void Update()
        {
            if (_keyboard != null && _keyboard.spaceKey.wasPressedThisFrame)
            {
                StateManager.Instance.SetState(State.PreGame);
                SceneManager.LoadScene(Scene.Map.ToString(), LoadSceneMode.Additive);
            }
        }
    }
}

