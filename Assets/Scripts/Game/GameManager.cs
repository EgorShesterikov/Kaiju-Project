using DG.Tweening;
using Game.EnemyBlock.Controllers;
using UnityEngine;
using Zenject;

namespace Kaiju
{
    public class GameManager : MonoBehaviour
    {
        [Header("UI")] 
        [SerializeField] private HudManager hudManager;

        [Header("Game Play")]
        [SerializeField] private Player player;
        [SerializeField] private CombatRobot combatRobot;
        [SerializeField] private GameCamera gameCamera;
        [SerializeField] private EnemySpawner enemySpawner;

        [Inject] private readonly IInputController _inputController;
        [Inject] private readonly IHintController _hintController;

        private Tween _startPosRobotTween;

        private void Awake()
        {
            hudManager.OnGameStarted += GameStarted;
        }

        private void OnDestroy()
        {
            hudManager.OnGameStarted -= GameStarted;
        }

        private void Start()
        {
            hudManager.ShowMainWindow(null, true);
        }

        private void GameStarted()
        {
            _startPosRobotTween = combatRobot.transform.DOMoveY(0.1f, 2f)
                .SetEase(Ease.OutBack)
                .OnComplete(()
                =>
                {
                    _inputController.SetObjectControl(player);

                    combatRobot.Activated();
                    gameCamera.SetTowardCameraInRobot(true);
                    enemySpawner.gameObject.SetActive(true);
                });
        }
    }
}