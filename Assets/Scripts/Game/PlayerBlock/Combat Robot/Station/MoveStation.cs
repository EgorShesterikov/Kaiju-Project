using DG.Tweening;
using UnityEngine;

namespace Kaiju
{
    public class MoveStation : StationBase, IController
    {
        private const float NEED_TIME_NO_INPUT_TO_DE_ACTIVE_ENGIE = 0.05f;

        private const float DE_ACTIVE_Y_FVX_ENGINE = 0.535f;
        private const float ACTIVE_Y_FVX_ENGINE = -0.8f;

        [SerializeField] private CombatRobot combatRobot;

        [Space]
        [SerializeField] private Transform leftEngine;
        [SerializeField] private Transform rightEngine;
        [SerializeField] private Transform leftFvxEngine;
        [SerializeField] private Transform rightFvxEngine;

        [Space] 
        [SerializeField] private MoveStationConfig config;

        private bool _isActiveEngine;
        private bool _isDeActiveProcess;
        private float _engineRotation;

        private float _currentSpeed;

        private Tween _changeMoveTween;
        private Tween _changeRotationTween;
        private Sequence _defaultRotationEngineSequence;
        private Sequence _changeFvxEngineSequence;

        private float _startCombatRobotYPosition;

        private float _currentTimeNoInputToDeActiveEngine;

        private void Awake()
        {
            _startCombatRobotYPosition = combatRobot.transform.position.y;
        }

        public void StartControl()
        {
            _hintController.SetTargetHintControl(config.HintControlData);
        }

        public override void PressInstantHorizontal(float value)
        {
            RotateEngine(value);

            if (!Mathf.Approximately(value, 0))
            {
                _currentTimeNoInputToDeActiveEngine = 0;

                if (!_isActiveEngine || _isDeActiveProcess)
                {
                    IsActiveEngine();
                }
            }
            else
            {
                if (_currentTimeNoInputToDeActiveEngine < NEED_TIME_NO_INPUT_TO_DE_ACTIVE_ENGIE)
                {
                    _currentTimeNoInputToDeActiveEngine += Time.deltaTime;
                }
                else if (!_isDeActiveProcess)
                {
                    SetDefaultRotationEngine();
                    IsDeActiveEngine();
                }
            }
        }

        public override void Enter(IController player)
        {
            base.Enter(player);

            _defaultRotationEngineSequence?.Kill();
        }

        protected override void Exit()
        {
            base.Exit();

            SetDefaultRotationEngine();
            IsDeActiveEngine();
        }

        private void IsActiveEngine()
        {
            _isActiveEngine = true;

            _defaultRotationEngineSequence?.Kill();

            _changeMoveTween?.Kill();
            _changeMoveTween = DOVirtual.Float(_currentSpeed, config.MoveSpeed, config.TimeToFullMove,
                value =>
                {
                    _currentSpeed = value;
                })
                .SetEase(Ease.Linear).SetAutoKill(this);

            _changeFvxEngineSequence?.Kill();
            _changeFvxEngineSequence = DOTween.Sequence();

            var currentProgress = leftFvxEngine.transform.localPosition.y - ACTIVE_Y_FVX_ENGINE;
            var offsetProgressTime = Mathf.Lerp(DE_ACTIVE_Y_FVX_ENGINE, ACTIVE_Y_FVX_ENGINE, currentProgress);

            _changeFvxEngineSequence.Append(leftFvxEngine.DOLocalMoveY(ACTIVE_Y_FVX_ENGINE, config.TimeToFullMove - offsetProgressTime))
                .Join(rightFvxEngine.DOLocalMoveY(ACTIVE_Y_FVX_ENGINE, config.TimeToFullMove - offsetProgressTime))
                .SetEase(Ease.Linear).SetAutoKill(this);

            _changeRotationTween?.Kill();
        }

        private void IsDeActiveEngine()
        {
            _isDeActiveProcess = true;

            _changeMoveTween?.Kill();
            _changeMoveTween = DOVirtual.Float(_currentSpeed, 0, config.TimeToStopMove,
                    value =>
                    {
                        _currentSpeed = value;
                    }).OnKill(() =>
                {
                    _isDeActiveProcess = false;
                    _isActiveEngine = false;
                })
                .SetEase(Ease.Linear).SetAutoKill(this);

            _changeFvxEngineSequence?.Kill();
            _changeFvxEngineSequence = DOTween.Sequence();

            var currentProgress = leftFvxEngine.transform.localPosition.y - DE_ACTIVE_Y_FVX_ENGINE;
            var offsetProgressTime = Mathf.Lerp(DE_ACTIVE_Y_FVX_ENGINE, ACTIVE_Y_FVX_ENGINE, currentProgress);

            _changeFvxEngineSequence.Append(leftFvxEngine.DOLocalMoveY(DE_ACTIVE_Y_FVX_ENGINE, config.TimeToStopMove - offsetProgressTime))
                .Join(rightFvxEngine.DOLocalMoveY(DE_ACTIVE_Y_FVX_ENGINE, config.TimeToStopMove - offsetProgressTime))
                .SetEase(Ease.Linear).SetAutoKill(this);

            _changeRotationTween?.Kill();
            _changeRotationTween = combatRobot.transform.DORotate(Vector3.zero, config.TimeToStopMove);
        }

        private void SetDefaultRotationEngine()
        {
            _defaultRotationEngineSequence?.Kill();
            _defaultRotationEngineSequence = DOTween.Sequence();

            _defaultRotationEngineSequence.OnUpdate(() => _engineRotation = leftEngine.localRotation.z)
                .Append(leftEngine.transform.DOLocalRotateQuaternion(Quaternion.identity, config.TimeToStopMove))
                .Join(rightEngine.transform.DOLocalRotateQuaternion(Quaternion.identity, config.TimeToStopMove))
                .SetAutoKill(this);
        }

        private void FixedUpdate()
        {
            CalculateSwing();
            CalculateForce();
        }

        private void CalculateSwing()
        {
            var targetPosition = Vector3.zero;
            var swing = Mathf.Sin(Time.time * config.SwingSpeed) * config.SwingAmplitude;
            targetPosition.y = _startCombatRobotYPosition + swing - combatRobot.transform.position.y;
            combatRobot.transform.position += targetPosition;
        }

        private void CalculateForce()
        {
            if (!_isActiveEngine) return;

            var targetPosition = Vector3.right * -_engineRotation * _currentSpeed;
            combatRobot.transform.position += targetPosition;

            var targetRotation = Quaternion.Euler(0, 0, -_engineRotation * config.MaxCombatRobotRotationAngle);
            combatRobot.transform.rotation = Quaternion.RotateTowards(combatRobot.transform.rotation, targetRotation, _currentSpeed);
        }

        private void RotateEngine(float value)
        {
            var rotation = leftEngine.localRotation;
            rotation.z -= config.SpeedEngineRotation * value * Time.fixedDeltaTime;
            rotation.z = Mathf.Clamp(rotation.z, -config.ClampAngleEngineRotation, config.ClampAngleEngineRotation);

            _engineRotation = rotation.z;

            leftEngine.localRotation = rotation;
            rightEngine.localRotation = rotation;
        }
    }
}