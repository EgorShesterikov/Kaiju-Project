using UnityEngine;

namespace Kaiju
{
    [CreateAssetMenu(fileName = "CombatRobotConfig", menuName = "Configs/CombatRobotConfig")]
    public class CombatRobotConfig : ScriptableObject
    {
        [SerializeField] private float damageAnimDuration = 0.1f;
        [SerializeField] private float damageAnimStrange = 0.1f;
        [SerializeField] private float damageAnimMultiplier = 0.1f;

        public float DamageAnimDuration => damageAnimDuration;
        public float DamageAnimMultiplier => damageAnimMultiplier;
    }
}