using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Kaiju
{
    public class ArrowSlider : Slider
    {
        [SerializeField] private GameObject arrow;

        public override void OnSelect(BaseEventData eventData)
        {
            base.OnSelect(eventData);

            arrow.SetActive(true);
        }

        public override void OnDeselect(BaseEventData eventData)
        {
            base.OnDeselect(eventData);

            arrow.SetActive(false);
        }
    }
}