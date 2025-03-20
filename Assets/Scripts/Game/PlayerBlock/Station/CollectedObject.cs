using System;
using DG.Tweening;
using UnityEngine;

namespace Kaiju
{
    public class CollectedObject : MonoBehaviour, ICollected
    {
        [SerializeField] private float collectedDuration = 2;
        [SerializeField] private Rigidbody2D rigidbody2D;

        private float _liquidCount;

        private bool _isTacked;

        private Tween _collectedTween;

        public bool IsTacked => _isTacked;

        public void SetLiquidCount(float liquidCount)
        {
            _liquidCount = liquidCount;
        }

        public void Collected(Vector3 target, Action<float> callBack)
        {
            _isTacked = true;

            rigidbody2D.isKinematic = true;

            _collectedTween = transform.DOMove(target, collectedDuration)
                .OnComplete(() =>
                {
                    callBack.Invoke(_liquidCount);
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