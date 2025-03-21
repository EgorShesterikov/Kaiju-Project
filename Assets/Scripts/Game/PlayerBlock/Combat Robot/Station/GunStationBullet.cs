using DG.Tweening;
using EnemyBlock.Interfaces;
using UnityEngine;

namespace Kaiju
{
    public class GunStationBullet : MonoBehaviour
    {
        private float _speed;
        private float _damage;
        private Vector3 _direction;

        private Tween _autoDestroyTween;

        public void Initialize(float speed, float damage, float lifeTime, Vector3 direction)
        {
            _speed = speed;
            _damage = damage;
            _direction = direction;

            _autoDestroyTween = DOVirtual.DelayedCall(lifeTime, DestroyBullet).SetAutoKill(this);
        }

        private void FixedUpdate()
        {
            transform.position += _direction * _speed;
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other != null)
            {
                if (other.TryGetComponent(out IDamageable damageable))
                {
                    damageable.SetDamage(_damage);
                }

                _autoDestroyTween.Kill();
                DestroyBullet();
            }
        }

        private void DestroyBullet()
        {
            Destroy(gameObject);
        }
    }
}