using UnityEngine;

namespace Kaiju
{
    [CreateAssetMenu(fileName = "MoveStationConfig", menuName = "Configs/MoveStationConfig")]
    public class MoveStationConfig : ScriptableObject
    {
        [SerializeField] private float speedEngineRotation = 3;
        [SerializeField, Range(0, 1)] private float clampAngleEngineRotation = 0.5f;
        [SerializeField] private float powerEngine = 100;
        [SerializeField] private float maxVelocity = 5;
        [SerializeField] private float maxTimeToStopMove = 0.5f;

        public float SpeedEngineRotation => speedEngineRotation;
        public float ClampAngleEngineRotation => clampAngleEngineRotation;
        public float PowerEngine => powerEngine;
        public float MaxVelocity => maxVelocity;
        public float MaxTimeToStopMove => maxTimeToStopMove;
    }
}