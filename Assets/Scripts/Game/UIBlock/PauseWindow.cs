using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Kaiju
{
    public class PauseWindow : Window
    {
        public event Action OnResumeButtonClicked = delegate { };
        public event Action OnSettingsButtonClicked = delegate { };
        public event Action OnExitButtonClicked = delegate { };

        [SerializeField] private ArrowButton resumeButton;
        [SerializeField] private ArrowButton settingsButton;
        [SerializeField] private ArrowButton exitButton;

        private void Awake()
        {
            resumeButton.onClick.AddListener(ResumeButtonClick);
            settingsButton.onClick.AddListener(SettingsButtonClick);
            exitButton.onClick.AddListener(ExitButtonClick);
        }

        private void ResumeButtonClick()
        {
            OnResumeButtonClicked.Invoke();
        }

        private void SettingsButtonClick()
        {
            OnSettingsButtonClicked.Invoke();
        }

        private void ExitButtonClick()
        {
            OnExitButtonClicked.Invoke();
        }

        private void Update()
        {
            if (IsDisplay)
            {
                var selectedObject = EventSystem.current.currentSelectedGameObject;

                if (selectedObject != resumeButton.gameObject && selectedObject != settingsButton.gameObject && selectedObject != exitButton.gameObject)
                {
                    resumeButton.Select();
                }
            }
        }
    }
}