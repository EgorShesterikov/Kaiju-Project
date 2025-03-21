using UnityEngine;

namespace Kaiju
{
    [CreateAssetMenu(fileName = "MoveStationConfig", menuName = "Configs/MoveStationConfig")]
    public class MoveStationConfig : BaseStationConfig
    {
        [SerializeField] private float speedEngineRotation = 3;
        [SerializeField] private float maxCombatShipRotationAngle = 5f;

        [Space]
        [SerializeField] private float swingAmplitude = 0.1f;
        [SerializeField] private float swingSpeed = 10;
        [SerializeField, Range(0, 1)] private float clampAngleEngineRotation = 0.5f;

        [Space]
        [SerializeField] private float moveSpeed = 1;
        [SerializeField] private float timeToFullMove = 0.5f;
        [SerializeField] private float timeToStopMove = 0.5f;

        public float SpeedEngineRotation => speedEngineRotation;
        public float MaxCombatRobotRotationAngle => maxCombatShipRotationAngle;
        public float SwingAmplitude => swingAmplitude;
        public float SwingSpeed => swingSpeed;
        public float ClampAngleEngineRotation => clampAngleEngineRotation;
        public float MoveSpeed => moveSpeed;
        public float TimeToFullMove => timeToFullMove;
        public float TimeToStopMove => timeToStopMove;
    }
}