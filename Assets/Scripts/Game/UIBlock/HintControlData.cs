using System.Collections.Generic;
using UnityEngine;

namespace Kaiju
{
    [CreateAssetMenu(fileName = "HintControl", menuName = "Data/HintControl")]
    public class HintControlData : ScriptableObject
    {
        [SerializeField] private string nameContext;
        [SerializeField] private List<GameObject> hintCollection;

        public string NameContext => nameContext;
        public IReadOnlyList<GameObject> HintCollection => hintCollection;
    }
}