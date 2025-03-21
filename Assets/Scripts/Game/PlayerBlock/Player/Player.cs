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

        private Vector2 _moveDirection;
        private Tween _stopMoveTween;

        private bool _isStairs;
        private float _cacheGravity;

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
                RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, 0.1f);

                if (hit.collider != null)
                {
                    if (hit.collider.CompareTag("Ground") && value < 0)
                    {
                        return;
                    }
                }

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

            animator.SetBool(IS_MOVE_PARAM, true);

            TurnScale(moveDirection);
        }

        private void TurnScale(Vector2 moveDirection)
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
            _moveDirection = Vector2.zero;
            var timeToStopMove = Mathf.InverseLerp(0, config.MaxVelocity, Mathf.Abs(rigidbody.velocity.x)) * config.MaxTimeToStopMove;

            _stopMoveTween?.Kill();
            _stopMoveTween = DOVirtual.Float(rigidbody.velocity.x, 0, timeToStopMove,
                value =>
                {
                    var newVelocity = rigidbody.velocity;
                    newVelocity.x = value;

                    rigidbody.velocity = newVelocity;
                }).OnComplete(() => animator.SetBool(IS_MOVE_PARAM, false))
                .SetEase(Ease.Linear).SetAutoKill(this);
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
                _enterStation = null;
            }
        }
    }
}