using DG.Tweening;
using UnityEngine;

namespace Kaiju
{
    public class Player : MonoBehaviour, IController
    {
        [SerializeField] private PlayerConfig config;
        [SerializeField] private Rigidbody2D rigidbody;

        [Space]
        [SerializeField] private PlayerHintComponent hintComponent;

        private Vector2 _moveDirection;
        private Tween _stopMoveTween;

        private bool _isStairs;
        private float _cacheGravity;

        private StationBase _enterStation;

        public void PressInstantHorizontal(float value)
        {
            if (!Mathf.Approximately(value, 0))
            {
                var moveDirection = Vector2.right * value;
                StartMove(moveDirection);
            }
            else
            {
                StopMove();
            }
        }

        public void PressInstantVertical(float value)
        {
            if (_isStairs)
            {
                var newVelocity = rigidbody.velocity;

                newVelocity.y = value * config.StairsSpeed;

                rigidbody.velocity = newVelocity;
            }
        }

        public void PressSpace(bool active) { }

        public void PressE()
        {
            if (_enterStation != null)
            {
                hintComponent.DisplayPressE_Hint(false);

                _enterStation.Enter(this);
                StopMove();
            }
        }

        private void FixedUpdate()
        {
            CalculateVelocity();
        }

        private void StartMove(Vector2 moveDirection)
        {
            _stopMoveTween?.Kill();
            _moveDirection = moveDirection;
        }

        private void StopMove()
        {
            _moveDirection = Vector2.zero;
            var timeToStopMove = Mathf.InverseLerp(0, config.MaxVelocity, Mathf.Abs(rigidbody.velocity.x)) * config.MaxTimeToStopMove;

            _stopMoveTween?.Kill();
            _stopMoveTween = DOVirtual.Float(rigidbody.velocity.x, 0, timeToStopMove,
                value =>
                {
                    var newVelocity = rigidbody.velocity;
                    newVelocity.x = value;

                    rigidbody.velocity = newVelocity;
                }).SetEase(Ease.Linear).SetAutoKill(this);
        }

        private void CalculateVelocity()
        {
            if (_moveDirection == Vector2.zero) return;

            var velocity = rigidbody.velocity;

            var newVelocity = velocity + _moveDirection * config.MoveSpeed;

            newVelocity.x = Mathf.Clamp(newVelocity.x, -config.MaxVelocity, config.MaxVelocity);

            rigidbody.velocity = newVelocity;
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Stairs"))
            {
                _isStairs = true;
                _cacheGravity = rigidbody.gravityScale;
                rigidbody.gravityScale = 0;
            }

            if (other.TryGetComponent(out StationBase station))
            {
                hintComponent.DisplayPressE_Hint(true);

                _enterStation = station;
            }
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            if (other.CompareTag("Stairs"))
            {
                _isStairs = false;
                rigidbody.gravityScale = _cacheGravity;
            }

            if (other.TryGetComponent(out StationBase station))
            {
                hintComponent.DisplayPressE_Hint(false);

                _enterStation = null;
            }
        }
    }
}