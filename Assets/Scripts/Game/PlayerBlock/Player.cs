using DG.Tweening;
using UnityEngine;

namespace Kaiju
{
    public class Player : MonoBehaviour, IController
    {
        [SerializeField] private PlayerConfig _config;
        [SerializeField] private Rigidbody2D _rigidbody;

        [Space]
        [SerializeField] private GameObject pressE_Hint;

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
                var newVelocity = _rigidbody.velocity;

                newVelocity.y = value * _config.StairsSpeed;

                _rigidbody.velocity = newVelocity;
            }
        }

        public void PressSpace(bool active) { }

        public void PressE()
        {
            if (_enterStation != null)
            {
                DisplayPressE_Hint(false);

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
            var timeToStopMove = Mathf.InverseLerp(0, _config.MaxVelocity, Mathf.Abs(_rigidbody.velocity.x)) * _config.MaxTimeToStopMove;

            _stopMoveTween?.Kill();
            _stopMoveTween = DOVirtual.Float(_rigidbody.velocity.x, 0, timeToStopMove,
                value =>
                {
                    var newVelocity = _rigidbody.velocity;
                    newVelocity.x = value;

                    _rigidbody.velocity = newVelocity;
                }).SetEase(Ease.Linear).SetAutoKill(this);
        }

        private void CalculateVelocity()
        {
            if (_moveDirection == Vector2.zero) return;

            var velocity = _rigidbody.velocity;

            var newVelocity = velocity + _moveDirection * _config.MoveSpeed;

            newVelocity.x = Mathf.Clamp(newVelocity.x, -_config.MaxVelocity, _config.MaxVelocity);

            _rigidbody.velocity = newVelocity;
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Stairs"))
            {
                _isStairs = true;
                _cacheGravity = _rigidbody.gravityScale;
                _rigidbody.gravityScale = 0;
            }

            if (other.TryGetComponent(out StationBase station))
            {
                DisplayPressE_Hint(true);

                _enterStation = station;
            }
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            if (other.CompareTag("Stairs"))
            {
                _isStairs = false;
                _rigidbody.gravityScale = _cacheGravity;
            }

            if (other.TryGetComponent(out StationBase station))
            {
                DisplayPressE_Hint(false);

                _enterStation = null;
            }
        }

        private void DisplayPressE_Hint(bool value)
        {
            pressE_Hint.SetActive(value);
        }
    }
}