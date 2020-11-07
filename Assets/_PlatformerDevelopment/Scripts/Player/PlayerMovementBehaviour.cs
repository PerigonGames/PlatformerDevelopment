using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace PersonalDevelopment
{
    public class PlayerMovementBehaviour : MonoBehaviour
    {
        private PlayerCharacterBindings _bindings = null;

        // Start is called before the first frame update
        void OnEnable()
        {
            _bindings = new PlayerCharacterBindings();
            _bindings.Enable();
        }

        private void OnDisable()
        {
            _bindings.Disable();
        }

        public void OnMove(InputAction.CallbackContext context)
        {
            print("On Move Pressed");
        }

        public void OnFire(InputAction.CallbackContext context)
        {
            print("On Fire");
        }

        public void OnMelee(InputAction.CallbackContext context)
        {
            print("On Melee");
        }
    }
}