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
                var child = player.transform.GetComponentInChildren<Animator>();
                // The correct animator has to be placed into PlayerBehaviour
                var playerAnimation = player.GetComponent<PlayerAnimationBehaviour>();
                playerAnimation.Initialize(child);
                player.GetComponent<PlayerBehaviour>().SetupPlayerForGame();
                // Set the Player Animation and gameObject
                player.GetComponent<PlayerMeleeAttackBehaviour>().Initialize(playerAnimation, child.gameObject);
            }
        }
    }
}