using UnityEngine;

namespace Kaiju
{
    public class BaseStationConfig : ScriptableObject
    {
        [SerializeField] private HintControlData hintControlData;

        public HintControlData HintControlData => hintControlData;
    }
}