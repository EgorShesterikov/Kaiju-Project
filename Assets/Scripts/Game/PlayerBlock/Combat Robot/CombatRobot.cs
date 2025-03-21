using System;
using DG.Tweening;
using EnemyBlock.Interfaces;
using UnityEngine;

namespace Kaiju
{
    public class CombatRobot : MonoBehaviour, IDamageable
    {
        public event Action OnEvaluated = delegate { };
        public event Action OnDead = delegate { };

        [SerializeField] private SpriteRenderer liquidProgress;

        [SerializeField] private CombatRobotConfig config;

        private Sequence _damageSequence;

        private bool _isActive;
        private float _defaultYPosition;
        private float _activeLifeTime;

        public bool IsActive => _isActive;
        public float ActiveLifeTime => _activeLifeTime;

        public float DefaultYPosition => _defaultYPosition;

        public void Activated()
        {
            _defaultYPosition = transform.position.y;

            _isActive = true;
        }

        public void ChangeLiquid(float value)
        {
            var size = liquidProgress.size;
            size.x += value;

            size.x = Mathf.Clamp01(size.x);

            liquidProgress.size = size;

            CheckResultAction();
        }

        public void SetDamage(float damage = 0)
        {
            ChangeLiquid(-damage);
            PlayDamageAnim(damage);
        }

        private void PlayDamageAnim(float damage)
        {
            if (_damageSequence != null && _damageSequence.IsActive()) return;

            _damageSequence = DOTween.Sequence();

            var strange = Vector3.one * (damage * config.DamageAnimMultiplier);

            _damageSequence.Append(transform.DOShakePosition(config.DamageAnimDuration, strange))
                .Join(transform.DOShakeRotation(config.DamageAnimDuration, strange)).SetAutoKill(this);
        }

        public void Update()
        {
            if (IsActive)
            {
                _activeLifeTime += Time.deltaTime;
            }
        }

        private void CheckResultAction()
        {
            if (Mathf.Approximately(liquidProgress.size.x, 0))
            {
                OnDead.Invoke();
            }
            else if (Mathf.Approximately(liquidProgress.size.x, 1))
            {
                OnEvaluated.Invoke();
            }
        }
    }
}