using DG.Tweening;
using UnityEngine;

namespace Kaiju
{
    public class MoveStation : StationBase
    {
        [SerializeField] private CombatRobot combatRobot;

        [Space]
        [SerializeField] private Transform leftEngine;
        [SerializeField] private Transform rightEngine;

        [Space] 
        [SerializeField] private MoveStationConfig config;

        private bool _isActiveEngine;
        private float _engineRotation;

        private float _currentSpeed;

        private Tween _changeMoveTween;
        private Tween _changeRotationTween;
        private Sequence _defaultRotationEngineSequence;

        private float _startCombatRobotYPosition;

        private void Awake()
        {
            _startCombatRobotYPosition = combatRobot.transform.position.y;
        }

        public override void PressInstantHorizontal(float value)
        {
            RotateEngine(value);
        }

        public override void PressSpace(bool active)
        {
            if (active)
            {
                IsActiveEngine();
            }
            else
            {
                IsDeActiveEngine();
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

            _changeMoveTween?.Kill();
            _changeMoveTween = DOVirtual.Float(_currentSpeed, config.MoveSpeed, config.TimeToFullMove,
                value =>
                {
                    _currentSpeed = value;
                })
                .SetEase(Ease.Linear).SetAutoKill(this);

            _changeRotationTween?.Kill();
        }

        private void IsDeActiveEngine()
        {
            _changeMoveTween?.Kill();
            _changeMoveTween = DOVirtual.Float(_currentSpeed, 0, config.TimeToStopMove,
                    value =>
                    {
                        _currentSpeed = value;
                    }).OnComplete(() => _isActiveEngine = false)
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
            rotation.z += config.SpeedEngineRotation * value * Time.fixedDeltaTime;
            rotation.z = Mathf.Clamp(rotation.z, -config.ClampAngleEngineRotation, config.ClampAngleEngineRotation);

            _engineRotation = rotation.z;

            leftEngine.localRotation = rotation;
            rightEngine.localRotation = rotation;
        }
    }
}