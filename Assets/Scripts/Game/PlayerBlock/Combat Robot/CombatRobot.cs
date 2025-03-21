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
            if (Input.GetKeyDown(KeyCode.B))
            {
                SetDamage(0.035f);
            }

            if (Input.GetKeyDown(KeyCode.N))
            {
                SetDamage(0.1f);
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