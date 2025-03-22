using System;
using DG.Tweening;
using UnityEngine;

namespace Kaiju
{
    [RequireComponent(typeof(CanvasGroup))]
    public abstract class Window : MonoBehaviour
    {
        private const float CHANGE_ACTIVE_DURATION = 0.1f;

        [SerializeField] private CanvasGroup canvasGroup;

        protected bool _isActive;

        private Tween _changeActiveTween;

        public virtual void Show(Action callBack = null, bool instant = false)
        {
            _isActive = true;

            if (!instant)
            {

                _changeActiveTween?.Kill();
                _changeActiveTween = canvasGroup.DOFade(1, CHANGE_ACTIVE_DURATION)
                    .OnKill(() =>
                    {
                        canvasGroup.blocksRaycasts = true;
                        canvasGroup.interactable = true;
                        callBack?.Invoke();
                    });
            }
            else
            {
                canvasGroup.alpha = 1;
                canvasGroup.blocksRaycasts = true;
                canvasGroup.interactable = true;
                callBack?.Invoke();
            }
        }

        public virtual void Hide(Action callBack = null, bool instant = false)
        {
            _isActive = false;

            canvasGroup.blocksRaycasts = false;
            canvasGroup.interactable = false;

            if (!instant)
            {
                _changeActiveTween?.Kill();
                _changeActiveTween = canvasGroup.DOFade(0, CHANGE_ACTIVE_DURATION)
                    .OnKill(() => callBack?.Invoke());
            }
            else
            {
                canvasGroup.alpha = 0;
                callBack?.Invoke();
            }
        }
    }
}