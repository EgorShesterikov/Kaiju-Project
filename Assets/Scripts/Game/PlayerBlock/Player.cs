using DG.Tweening;
using UnityEngine;
using Zenject;

namespace Kaiju
{
    public class Player : MonoBehaviour, IController
    {
        [SerializeField] private PlayerConfig _config;
        [SerializeField] private Rigidbody2D _rigidbody;

        [Inject] private readonly IInputController _inputController;

        private Vector2 _moveDirection;
        private Tween _stopMoveTween;

        private bool _isGrounded;

        public void PressA(bool active)
        {
            SetMove(active, Vector2.left);
        }

        public void PressD(bool active)
        {
            SetMove(active, Vector2.right);
        }

        public void PressW(bool active)
        {
            Debug.LogError("W");
        }

        public void PressS(bool active)
        {
            Debug.LogError("S");
        }

        public void PressSpace()
        {
            _rigidbody.AddForce(Vector2.up * _config.JumpPower);
        }

        public void PressE()
        {
            Debug.LogError("E");
        }

        private void FixedUpdate()
        {
            CalculateVelocity();
        }

        private void SetMove(bool active, Vector2 targetVector)
        {
            if (active)
            {
                StartMove(targetVector);
            }
            else if (_moveDirection == targetVector)
            {
                StopMove();
            }
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

    }
}