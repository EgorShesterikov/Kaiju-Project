using UnityEngine;

namespace Kaiju
{
    public class Stairs : MonoBehaviour
    {
        [SerializeField] private float maxActivePosY;
        [SerializeField] private float minActivePosY;

        public float MaxActivePosY => maxActivePosY;
        public float MinActivePosY => minActivePosY;
    }
}