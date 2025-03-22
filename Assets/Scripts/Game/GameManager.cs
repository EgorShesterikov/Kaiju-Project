using DG.Tweening;
using Game.EnemyBlock.Controllers;
using UnityEngine;
using Zenject;

namespace Kaiju
{
    public class GameManager : MonoBehaviour
    {
        private const float DEFAULT_Y_POS_COMBAT_ROBOT = 0.1f;

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

        private float _startYPosCombatRobot;

        private void Awake()
        {
            _startYPosCombatRobot = combatRobot.transform.position.y;

            hudManager.OnGameStarted += GameStarted;
            hudManager.OnGameExit += GameExit;
        }

        private void OnDestroy()
        {
            hudManager.OnGameStarted -= GameStarted;
            hudManager.OnGameExit -= GameExit;
        }

        private void Start()
        {
            hudManager.ShowMainWindow(null, true);
        }

        private void GameStarted()
        {
            _startPosRobotTween = combatRobot.transform.DOMoveY(DEFAULT_Y_POS_COMBAT_ROBOT, 2f)
                .SetEase(Ease.OutBack)
                .OnComplete(()
                =>
                {
                    _inputController.SetObjectControl(player);

                    combatRobot.Activated(true);
                    gameCamera.SetTowardCameraInRobot(true);
                    enemySpawner.gameObject.SetActive(true);
                });
        }

        private void GameExit()
        {
            _startPosRobotTween?.Kill();

            _inputController.SetObjectControl(null);

            _startPosRobotTween = combatRobot.transform.DOMoveY(_startYPosCombatRobot, 1f)
                .SetEase(Ease.InBack)
                .OnComplete(()
                    =>
                {
                    combatRobot.Activated(false);
                    gameCamera.SetTowardCameraInRobot(false);
                    enemySpawner.gameObject.SetActive(false);
                });
        }
    }
}