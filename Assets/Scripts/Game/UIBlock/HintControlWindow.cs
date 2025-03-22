using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace Kaiju
{
    public class HintControlWindow : Window
    {
        [SerializeField] private TextMeshProUGUI baseContext;
        [SerializeField] private Transform targetInstantHints;

        private List<GameObject> _viewHintCollection = new();

        public void DisplayTargetHintControl(HintControlData hintControlData)
        {
            Hide(() =>
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

                Show();
            });
        }
    }
}