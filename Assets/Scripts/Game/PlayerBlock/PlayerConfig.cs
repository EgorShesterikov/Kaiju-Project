using UnityEngine;

namespace Kaiju
{
    [CreateAssetMenu(fileName = "PlayerConfig", menuName = "Configs/PlayerConfig")]
    public class PlayerConfig : ScriptableObject
    {
        [SerializeField] private float moveSpeed;
        [SerializeField] private float maxVelocity;
        [SerializeField] private float maxTimeToStopMove;

        [Space]
        [SerializeField] private float stairsSpeed;

        public float MoveSpeed => moveSpeed;
        public float MaxVelocity => maxVelocity;
        public float MaxTimeToStopMove => maxTimeToStopMove;

        public float StairsSpeed => stairsSpeed;
    }
}