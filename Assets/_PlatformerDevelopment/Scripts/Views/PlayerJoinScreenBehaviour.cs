using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

namespace PersonalDevelopment
{
    public class PlayerJoinScreenBehaviour : MonoBehaviour
    {
        private void Update()
        {
            if (Keyboard.current.spaceKey.wasPressedThisFrame)
            {
                StateManager.Instance.SetState(State.PreGame);
                SceneManager.LoadScene(Scene.Map.ToString(), LoadSceneMode.Additive);
            }
        }
    }
}

