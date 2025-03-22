using System;
using DG.Tweening;
using UnityEngine;

namespace Kaiju
{
    public class CreditsWindow : Window, IController
    {
        private const float START_Y_POS = -170;
        private const float FINISH_Y_POS = 2200;

        public event Action OnClosed = delegate { };

        [SerializeField] private float time_credits = 15;

        [SerializeField] private RectTransform credits;

        private Tween _creditsMove;

        public override void Show(Action callBack = null, bool instant = false)
        {
            credits.anchoredPosition = new Vector2(0, START_Y_POS);

            base.Show(callBack, instant);

            _creditsMove = credits.DOAnchorPosY(FINISH_Y_POS, time_credits)
                .OnComplete(() => OnClosed.Invoke())
                .SetEase(Ease.Linear);
        }

        public override void Hide(Action callBack = null, bool instant = false)
        {
            base.Hide(callBack, instant);

            _creditsMove?.Kill();
        }

        public void PressInstantVertical(float value)
        {
            if (!Mathf.Approximately(value, 0))
            {
                OnClosed.Invoke();
            }
        }

        public void PressInstantHorizontal(float value)
        {
            if (!Mathf.Approximately(value, 0))
            {
                OnClosed.Invoke();
            }
        }

        public void PressSpace(bool active)
        {
            OnClosed.Invoke();
        }

        public void PressE()
        {
            OnClosed.Invoke();
        }
    }
}