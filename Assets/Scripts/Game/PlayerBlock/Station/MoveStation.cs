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

        private Tween _stopMoveTween;

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
            _stopMoveTween?.Kill();

            _isActiveEngine = true;
        }

        private void IsDeActiveEngine()
        {
            _isActiveEngine = false;

            var timeToStopMove = Mathf.InverseLerp(0, config.MaxVelocity, Mathf.Abs(combatRobot.Rigidbody2D.velocity.x)) * config.MaxTimeToStopMove;

            _stopMoveTween?.Kill();
            _stopMoveTween = DOVirtual.Float(combatRobot.Rigidbody2D.velocity.x, 0, timeToStopMove,
                    value =>
                    {
                        var newVelocity = combatRobot.Rigidbody2D.velocity;
                        newVelocity.x = value;

                        combatRobot.Rigidbody2D.velocity = newVelocity;
                    })
                .SetEase(Ease.Linear).SetAutoKill(this);
        }

        private void FixedUpdate()
        {
            CalculateForce();
        }

        private void CalculateForce()
        {
            if (!_isActiveEngine) return;

            combatRobot.Rigidbody2D.AddForce(Vector2.right * -_engineRotation * config.PowerEngine);

            var newVelocity = combatRobot.Rigidbody2D.velocity;
            newVelocity.x = Mathf.Clamp(newVelocity.x, -config.MaxVelocity, config.MaxVelocity);
            combatRobot.Rigidbody2D.velocity = newVelocity;
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