using System;
using DG.Tweening;
using UnityEngine;

namespace Kaiju
{
    [RequireComponent(typeof(CanvasGroup))]
    public abstract class Window : MonoBehaviour
    {
        [SerializeField] private CanvasGroup canvasGroup;
        [SerializeField] private float changeActiveDuration = 0.1f;

        private bool _isDisplay;

        private Tween _changeActiveTween;

        public bool IsDisplay => _isDisplay;

        public virtual void Show(Action callBack = null, bool instant = false)
        {
            _isDisplay = true;

            if (!instant)
            {

                _changeActiveTween?.Kill();
                _changeActiveTween = canvasGroup.DOFade(1, changeActiveDuration).SetUpdate(true)
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
            _isDisplay = false;

            canvasGroup.blocksRaycasts = false;
            canvasGroup.interactable = false;

            if (!instant)
            {
                _changeActiveTween?.Kill();
                _changeActiveTween = canvasGroup.DOFade(0, changeActiveDuration).SetUpdate(true)
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