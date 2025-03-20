using UnityEngine;

namespace Kaiju
{
    public class BackGroundTransformer : MonoBehaviour
    {
        private const float MULTIPLIER = 0.1f;

        [SerializeField] private Transform target;
        [SerializeField, Min(0)] private float speed;

        private float _lastTargetX;

        private void Awake()
        {
            _lastTargetX = target.position.x;
        }

        private void FixedUpdate()
        {
            if (!Mathf.Approximately(target.position.x, _lastTargetX))
            {
                float deltaX = target.position.x - _lastTargetX;

                transform.position += new Vector3(-deltaX * speed * MULTIPLIER, 0, 0);

                _lastTargetX = target.position.x;
            }
        }
    }
}