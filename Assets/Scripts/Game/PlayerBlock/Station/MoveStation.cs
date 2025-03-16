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

        private float _currentEngie;

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
            _changeMoveTween = DOVirtual.Float(_currentEngie, config.PowerEngine, config.TimeToFullMove,
                value =>
                {
                    _currentEngie = value;
                })
                .SetEase(Ease.Linear).SetAutoKill(this);
        }

        private void IsDeActiveEngine()
        {
            _isActiveEngine = false;

            _changeMoveTween?.Kill();
            _changeMoveTween = DOVirtual.Float(_currentEngie, 0, config.TimeToStopMove,
                    value =>
                    {
                        _currentEngie = value;
                        CalculateForce();
                    })
                .SetEase(Ease.Linear).SetAutoKill(this);
        }

        private void FixedUpdate()
        {
            if (_isActiveEngine)
            {
                CalculateForce();
            }
        }

        private void CalculateForce()
        {
            combatRobot.transform.position += Vector3.right * -_engineRotation * _currentEngie;
        }

        private void RotateEngine(float value)
        {
            var rotation = leftEngine.rotation;
            rotation.z += config.SpeedEngineRotation * value * Time.fixedDeltaTime;
            rotation.z = Mathf.Clamp(rotation.z, -config.ClampAngleEngineRotation, config.ClampAngleEngineRotation);

            _engineRotation = rotation.z;

            leftEngine.rotation = rotation;
            rightEngine.rotation = rotation;
        }
    }
}