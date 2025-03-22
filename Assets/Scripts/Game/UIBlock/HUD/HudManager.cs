using System;
using UnityEngine;
using Zenject;

namespace Kaiju
{
    public class HudManager : MonoBehaviour
    {
        private enum UIStateTypes
        {
            Main = 0,
            GamePlay = 1
        }

        public event Action OnGameStarted = delegate { };

        [SerializeField] private BlackPanelWindow blackPanelWindow;
        [SerializeField] private MainWindow mainWindow;
        [SerializeField] private SettingWindow settingsWindow;
        [SerializeField] private CreditsWindow creditsWindow;
        [SerializeField] private HintControlWindow hintControlWindow;

        [Inject] private readonly IHintController _hintController;
        [Inject] private readonly IInputController _inputController;

        private UIStateTypes _currentStateType = UIStateTypes.Main;

        private void Awake()
        {
            mainWindow.OnPlayButtonClicked += GameStarted;
            mainWindow.OnSettingsButtonClicked += ShowSettingsWindowInMain;
            mainWindow.OnCreditsButtonClicked += ShowCreditsWindow;

            settingsWindow.OnBackButtonClicked += HideSettingsWindow;

            creditsWindow.OnClosed += HideCreditsWindow;

            _hintController.OnSetTargetHintControl += SetTargetHintControl;
        }

        private void OnDestroy()
        {
            mainWindow.OnPlayButtonClicked -= GameStarted;
            mainWindow.OnSettingsButtonClicked -= ShowSettingsWindowInMain;
            mainWindow.OnCreditsButtonClicked -= ShowCreditsWindow;

            settingsWindow.OnBackButtonClicked -= HideSettingsWindow;

            creditsWindow.OnClosed -= HideCreditsWindow;

            _hintController.OnSetTargetHintControl -= SetTargetHintControl;
        }

        public void ShowMainWindow(Action callBack, bool instant = false)
        {
            blackPanelWindow.Show(null, instant);
            mainWindow.Show(null, instant);
        }

        private void GameStarted()
        {
            blackPanelWindow.Hide();
            mainWindow.Hide();

            _currentStateType = UIStateTypes.GamePlay;

            OnGameStarted.Invoke();
        }

        private void ShowSettingsWindowInMain()
        {
            mainWindow.Hide(() => settingsWindow.Show());
        }

        private void ShowSettingsWindowInGamePlay()
        {

        }

        private void HideSettingsWindow()
        {
            if (_currentStateType == UIStateTypes.Main)
            {
                settingsWindow.Hide(() => mainWindow.Show());
            }
            else
            {
                settingsWindow.Hide();
            }
        }

        private void ShowCreditsWindow()
        {
            mainWindow.Hide(() => creditsWindow.Show(() => _inputController.SetObjectControl(creditsWindow)));
            
        }

        private void HideCreditsWindow()
        {
            _inputController.SetObjectControl(null);
            creditsWindow.Hide(() => mainWindow.Show());
        }

        private void SetTargetHintControl(HintControlData hintControlData)
        {
            hintControlWindow.DisplayTargetHintControl(hintControlData);
        }
    }
}
