using System;
using DG.Tweening;
using UnityEngine;

namespace Kaiju
{
    public class CollectedObject : MonoBehaviour, ICollected
    {
        [SerializeField] private Rigidbody2D rigidbody2D;

        private bool _isTacked;

        private Tween _collectedTween;

        public bool IsTacked => _isTacked;

        public void Collected(Vector3 target, Action callBack)
        {
            _isTacked = true;

            rigidbody2D.isKinematic = true;

            _collectedTween = transform.DOMove(target, 2)
                .OnComplete(() =>
                {
                    callBack.Invoke();
                    Destroy(gameObject);
                })
                .SetAutoKill(this);
        }

        public void Drop()
        {
            _isTacked = false;

            rigidbody2D.isKinematic = false;

            _collectedTween?.Kill();
        }
    }
}