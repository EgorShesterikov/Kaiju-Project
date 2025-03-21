using DG.Tweening;
using UnityEngine;
using Zenject;

namespace Kaiju
{
    public class Player : MonoBehaviour, IController
    {
        private const string IS_MOVE_PARAM = "IsMove";

        [SerializeField] private PlayerConfig config;
        [SerializeField] private Rigidbody2D rigidbody;
        [SerializeField] private Animator animator;

        private Vector3 _velocity;
        private Vector3 _moveDirection;
        private Tween _stopMoveTween;
        private float _cacheGravity;

        private Stairs _enterStairs;
        private StationBase _enterStation;

        [Inject] private readonly IHintController _hintController;

        public void StartControl()
        {
            _hintController.SetTargetHintControl(config.HintControlData);
        }

        public void PressInstantHorizontal(float value)
        {
            if (!Mathf.Approximately(value, 0))
            {
                var moveDirection = Vector3.right * value;
                StartMove(moveDirection);
            }
            else
            {
                StopMove();
            }
        }

        public void PressInstantVertical(float value)
        {
            if (_enterStairs)
            {
                if (transform.localPosition.y >= _enterStairs.MaxActivePosY && value > 0 
                    || transform.localPosition.y <= _enterStairs.MinActivePosY && value < 0)
                    return;

                var newVelocity = _velocity;

                newVelocity.y = value * config.StairsSpeed;

                _velocity = newVelocity;
            }
            else
            {
                _velocity.y = 0;
            }
        }

        public void PressSpace(bool active) { }

        public void PressE()
        {
            if (_enterStation != null)
            {
                _enterStation.Enter(this);
                StopMove();
            }
        }

        private void FixedUpdate()
        {
            CalculateVelocity();

            rigidbody.velocity = Vector2.zero;
            transform.localPosition += _velocity;
        }

        private void StartMove(Vector3 moveDirection)
        {
            _stopMoveTween?.Kill();
            _moveDirection = moveDirection;

            animator.SetBool(IS_MOVE_PARAM, true);

            TurnScale(moveDirection);
        }

        private void TurnScale(Vector3 moveDirection)
        {
            var localScale = transform.localScale;

            if (moveDirection.x > 0 && localScale.x < 0
                || moveDirection.x < 0 && localScale.x > 0)
            {
                localScale.x *= -1;
            }

            transform.localScale = localScale;
        }

        private void StopMove()
        {
            _moveDirection = Vector3.zero;
            var timeToStopMove = Mathf.InverseLerp(0, config.MaxVelocity, Mathf.Abs(_velocity.x)) * config.MaxTimeToStopMove;

            _stopMoveTween?.Kill();
            _stopMoveTween = DOVirtual.Float(_velocity.x, 0, timeToStopMove,
                value =>
                {
                    var newVelocity = _velocity;
                    newVelocity.x = value;

                    _velocity = newVelocity;
                }).OnComplete(() => animator.SetBool(IS_MOVE_PARAM, false))
                .SetEase(Ease.Linear).SetAutoKill(this);
        }

        private void CalculateVelocity()
        {
            if (_moveDirection == Vector3.zero) return;

            var velocity = _velocity;

            var newVelocity = velocity + _moveDirection * config.MoveSpeed;

            newVelocity.x = Mathf.Clamp(newVelocity.x, -config.MaxVelocity, config.MaxVelocity);

            _velocity = newVelocity;
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.TryGetComponent(out Stairs stairs))
            {
                _enterStairs = stairs;

                _cacheGravity = rigidbody.gravityScale;
                rigidbody.gravityScale = 0;
            }

            if (other.TryGetComponent(out StationBase station))
            {
                _enterStation = station;
            }
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            if (other.TryGetComponent(out Stairs stairs))
            {
                _enterStairs = null;

                rigidbody.gravityScale = _cacheGravity;
            }

            if (other.TryGetComponent(out StationBase station))
            {
                _enterStation = null;
            }
        }
    }
}