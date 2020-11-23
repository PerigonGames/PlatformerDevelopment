using UnityEngine;
using UnityEngine.InputSystem;

namespace PersonalDevelopment
{
    public class SandBoxWorldManager : MonoBehaviour
    {
        // Start is called before the first frame update
        void Start()
        {
            var players = PlayerInput.all;
            foreach (var player in players)
            {
                player.GetComponent<PlayerBehaviour>().SetupPlayerForGame();
            }
        }
    }
}