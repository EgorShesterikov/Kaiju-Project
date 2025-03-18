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

        protected override void Exit()
        {
            base.Exit();

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
        }

        private void FixedUpdate()
        {
            CalculateForce();
        }

        private void CalculateForce()
        {
            if (!_isActiveEngine) return;

            combatRobot.transform.position += Vector3.right * -_engineRotation * _currentSpeed;
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