using UnityEngine;

namespace Kaiju
{
    [CreateAssetMenu(fileName = "CollectingStationConfig", menuName = "Configs/CollectingStationConfig")]
    public class CollectingStationConfig : ScriptableObject
    {
        [SerializeField] private float changeActiveDuration = 1;

        public float ChangeActiveDuration => changeActiveDuration;
    }
}