using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Kaiju
{
    public class SettingWindow : Window
    {
        public event Action<float> OnSoundValueChanged = delegate { };
        public event Action<float> OnMusicValueChanged = delegate { };
        public event Action OnBackButtonClicked = delegate { };

        [SerializeField] private ArrowSlider soundSlider;
        [SerializeField] private ArrowSlider musicSlider;
        [SerializeField] private ArrowButton backButton;

        private void Awake()
        {
            soundSlider.onValueChanged.AddListener(SoundValueChange);
            musicSlider.onValueChanged.AddListener(MusicValueChange);
            backButton.onClick.AddListener(BackButtonClick);
        }

        private void SoundValueChange(float value)
        {
            OnSoundValueChanged.Invoke(value);
        }

        private void MusicValueChange(float value)
        {
            OnMusicValueChanged.Invoke(value);
        }

        private void BackButtonClick()
        {
            OnBackButtonClicked.Invoke();
        }

        private void Update()
        {
            if (_isActive)
            {
                var selectedObject = EventSystem.current.currentSelectedGameObject;

                if (selectedObject != soundSlider.gameObject && selectedObject != musicSlider.gameObject && selectedObject != backButton.gameObject)
                {
                    soundSlider.Select();
                }
            }
        }
    }
}