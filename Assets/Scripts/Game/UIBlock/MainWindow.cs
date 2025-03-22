using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Kaiju
{
    public class MainWindow : Window
    {
        public event Action OnPlayButtonClicked = delegate { };
        public event Action OnSettingsButtonClicked = delegate { };
        public event Action OnCreditsButtonClicked = delegate { };

        [SerializeField] private ArrowButton playButton;
        [SerializeField] private ArrowButton settingsButton;
        [SerializeField] private ArrowButton creditsButton;

        private void Awake()
        {
            playButton.onClick.AddListener(PlayButtonClick);
            settingsButton.onClick.AddListener(SettingsButtonClick);
            creditsButton.onClick.AddListener(CreditsButtonClick);
        }

        private void PlayButtonClick()
        {
            OnPlayButtonClicked.Invoke();
        }

        private void SettingsButtonClick()
        {
            OnSettingsButtonClicked.Invoke();
        }

        private void CreditsButtonClick()
        {
            OnCreditsButtonClicked.Invoke();
        }

        private void Update()
        {
            if (_isActive)
            {
                var selectedObject = EventSystem.current.currentSelectedGameObject;

                if (selectedObject != playButton.gameObject && selectedObject != settingsButton.gameObject && selectedObject != creditsButton.gameObject)
                {
                    playButton.Select();
                }
            }
        }
    }
}