using UnityEngine;

namespace Kaiju
{
    public class GameCamera : MonoBehaviour
    {
        [SerializeField] private Transform target;
        [SerializeField] private Vector3 offset;
        [SerializeField] private float smoothSpeed = 0.125f;

        private bool _isActiveToward;

        public void SetTowardCameraInRobot(bool value)
        {
            _isActiveToward = value;
        }

        private void FixedUpdate()
        {
            if (!_isActiveToward) return;

            Vector3 desiredPosition = target.position + offset;
            desiredPosition.z = transform.position.z;

            Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);

            transform.position = smoothedPosition;
        }
    }
}