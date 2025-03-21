using DG.Tweening;
using Game.EnemyBlock.Controllers;
using UnityEngine;
using Zenject;

namespace Kaiju
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField] private Player player;
        [SerializeField] private CombatRobot combatRobot;
        [SerializeField] private GameCamera gameCamera;
        [SerializeField] private EnemySpawner enemySpawner;

        [Inject] private readonly IInputController _inputController;
        [Inject] private readonly IHintController _hintController;

        private Tween _startPosRobotTween;

        private void Start()
        {
            StartPlay();
        }

        private void StartPlay()
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