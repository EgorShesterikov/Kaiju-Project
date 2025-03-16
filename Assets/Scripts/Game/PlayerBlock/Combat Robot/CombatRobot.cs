using UnityEngine;

namespace Kaiju
{
    public class CombatRobot : MonoBehaviour
    {
        [SerializeField] private Rigidbody2D rigidbody2D;

        public Rigidbody2D Rigidbody2D => rigidbody2D;
    }
}