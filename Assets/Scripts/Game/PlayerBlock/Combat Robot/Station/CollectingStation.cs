using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

namespace Kaiju
{
    public class CollectingStation : StationBase
    {
        [SerializeField] private CombatRobot combatRobot;
        [SerializeField] private SpriteRenderer ray;
        [SerializeField] private Transform collectTarget;

        [Space]
        [SerializeField] private CollectingStationConfig config;

        private Tween _changedRayTween;

        private List<ICollected> _collectedList = new();

        public override void PressSpace(bool active)
        {
            if (active)
            {
                IsActiveRay();
            }
            else
            {
                IsDeActiveRay();
            }
        }

        private void IsActiveRay()
        {
            _changedRayTween?.Kill();
            _changedRayTween = ray.DOColor(Color.white, config.ChangeActiveDuration);

            foreach (var item in _collectedList)
            {
                if (!item.IsTacked)
                {
                    item.Collected(collectTarget.position, Collect);
                }
            }
        }

        private void Collect(float liquidCount)
        {
            combatRobot.ChangeLiquid(liquidCount);
        }

        private void IsDeActiveRay()
        {
            _changedRayTween?.Kill();
            _changedRayTween = ray.DOColor(Color.clear, config.ChangeActiveDuration);

            foreach (var item in _collectedList)
            {
                if (item.IsTacked)
                {
                    item.Drop();
                }
            }
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.TryGetComponent(out ICollected collected))
            {
                _collectedList.Add(collected);
            }
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            if (other.TryGetComponent(out ICollected collected))
            {
                _collectedList.Remove(collected);
            }
        }
    }
}