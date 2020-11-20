using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace PersonalDevelopment
{
    [RequireComponent(typeof(Rigidbody))]
    public class PlayerBehaviour : MonoBehaviour
    {
        private Rigidbody _rigidbody = null;
        private PlayerInput _playerInput = null;

        [SerializeField] private List<GameObject> _playerModels = null;

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

        public void SetupPlayer()
        {
            //TODO - Set player position
            _rigidbody.isKinematic = false;
        }

        private void Awake()
        {
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