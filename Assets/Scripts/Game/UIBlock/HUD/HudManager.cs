using System;
using DG.Tweening;
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
        public event Action OnGameExit = delegate { };

        [SerializeField] private BlackPanelWindow blackPanelWindow;
        [SerializeField] private MainWindow mainWindow;
        [SerializeField] private SettingWindow settingsWindow;
        [SerializeField] private CreditsWindow creditsWindow;
        [SerializeField] private PauseWindow pauseWindow;
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

            pauseWindow.OnResumeButtonClicked += HidePauseWindow;
            pauseWindow.OnSettingsButtonClicked += ShowSettingsWindowInGamePlay;
            pauseWindow.OnExitButtonClicked += GameExit;

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

        private void Update()
        {
            CheckActivatedPauseWindow();
        }

        private void CheckActivatedPauseWindow()
        {
            if (_currentStateType == UIStateTypes.GamePlay && Input.GetKeyDown(KeyCode.Escape) && Mathf.Approximately(Time.timeScale, 1))
            {
                ShowPauseWindow();
            }
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

            DOVirtual.DelayedCall(2.1f, () =>
            {
                _currentStateType = UIStateTypes.GamePlay;
            });

            OnGameStarted.Invoke();
        }

        private void GameExit()
        {
            Time.timeScale = 1;

            blackPanelWindow.Hide();
            pauseWindow.Hide();

            DOVirtual.DelayedCall(1.5f, () =>
            {
                blackPanelWindow.Show();
                mainWindow.Show();
            });

            _currentStateType = UIStateTypes.Main;

            OnGameExit.Invoke();
        }

        private void ShowSettingsWindowInMain()
        {
            mainWindow.Hide(() => settingsWindow.Show());
        }

        private void ShowSettingsWindowInGamePlay()
        {
            pauseWindow.Hide(() => settingsWindow.Show());
        }

        private void ShowPauseWindow()
        {
            Time.timeScale = 0;

            hintControlWindow.Hide();
            blackPanelWindow.Show();
            pauseWindow.Show();
        }

        private void HidePauseWindow()
        {
            Time.timeScale = 1;

            hintControlWindow.Show();
            blackPanelWindow.Hide();
            pauseWindow.Hide();
        }

        private void HideSettingsWindow()
        {
            if (_currentStateType == UIStateTypes.Main)
            {
                settingsWindow.Hide(() => mainWindow.Show());
            }
            else
            {
                settingsWindow.Hide(() => pauseWindow.Show());
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
