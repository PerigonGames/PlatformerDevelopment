using System;
using UnityEngine;
using UnityEngine.UI;

namespace PersonalDevelopment
{
    public class StartScreenBehaviour : MonoBehaviour
    {
        public event Action OnButtonPressed;
        
        [SerializeField] private Button _startButton = null;

        public void Initialize()
        {
            _startButton.onClick.AddListener(() =>
            {
                if (OnButtonPressed != null)
                {
                    OnButtonPressed();
                }
            });
        }

        public void CleanUp()
        {
            _startButton.onClick.RemoveAllListeners();
        }
    }
}