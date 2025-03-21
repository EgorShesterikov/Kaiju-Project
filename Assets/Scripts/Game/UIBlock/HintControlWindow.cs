using System;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;
using Zenject;

namespace Kaiju
{
    public class HintControlWindow : MonoBehaviour
    {
        private const float CHANGE_ACTIVE_DURATION = 0.1f;

        [SerializeField] private CanvasGroup canvasGroup;

        [SerializeField] private TextMeshProUGUI baseContext;
        [SerializeField] private Transform targetInstantHints;

        private List<GameObject> _viewHintCollection = new();

        private bool _isActive;

        private Tween _changeActiveTween;

        [Inject] private readonly IHintController _hintController;

        private void Awake()
        {
            _hintController.OnSetTargetHintControl += DisplayTargetHintControl;
        }

        private void OnDestroy()
        {
            _hintController.OnSetTargetHintControl -= DisplayTargetHintControl;
        }

        private void SetActive(bool value, Action callBack = null)
        {
            _isActive = value;

            _changeActiveTween?.Kill();
            _changeActiveTween = canvasGroup.DOFade(Convert.ToInt16(_isActive), CHANGE_ACTIVE_DURATION)
                .OnKill(() => callBack?.Invoke());
        }

        private void DisplayTargetHintControl(HintControlData hintControlData)
        {
            SetActive(false, () =>
            {
                foreach (var hint in _viewHintCollection)
                {
                    Destroy(hint);
                }

                _viewHintCollection.Clear();

                baseContext.text = hintControlData.NameContext;

                foreach (var hint in hintControlData.HintCollection)
                {
                    _viewHintCollection.Add(Instantiate(hint, targetInstantHints));
                }

                SetActive(true);
            });
        }
    }
}