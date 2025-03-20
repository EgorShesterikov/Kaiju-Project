using System;
using EnemyBlock.Interfaces;
using UnityEngine;

namespace Kaiju
{
    public class CombatRobot : MonoBehaviour, IDamageable
    {
        public event Action OnEvaluated = delegate { };
        public event Action OnDead = delegate { };

        [SerializeField] private SpriteRenderer liquidProgress;

        public void ChangeLiquid(float value)
        {
            value = Mathf.Clamp01(value);

            var size = liquidProgress.size;
            size.x += value;

            liquidProgress.size = size;

            CheckResultAction();
        }

        public void SetDamage(float damage = 0)
        {
            ChangeLiquid(damage);
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