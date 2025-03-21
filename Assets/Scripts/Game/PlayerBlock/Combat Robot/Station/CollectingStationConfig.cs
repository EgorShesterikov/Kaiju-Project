using UnityEngine;

namespace Kaiju
{
    [CreateAssetMenu(fileName = "CollectingStationConfig", menuName = "Configs/CollectingStationConfig")]
    public class CollectingStationConfig : BaseStationConfig
    {
        [SerializeField] private float changeActiveDuration = 1;

        public float ChangeActiveDuration => changeActiveDuration;
    }
}