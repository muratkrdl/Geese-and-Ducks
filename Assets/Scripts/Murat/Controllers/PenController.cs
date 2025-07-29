using UnityEngine;

namespace Murat.Controllers
{
    public class PenController : MonoBehaviour
    {
        [SerializeField] private float rotationSpeed;

        private Vector3 _goPos;

        private void Update()
        {
            RotateTowardsTarget();
        }

        private void RotateTowardsTarget()
        {
            Vector2 direction = _goPos - transform.position;

            if (direction.sqrMagnitude < 0.01f) return;

            float targetAngle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

            float finalAngle = GetAdjustedAngle(targetAngle);

            Quaternion targetRotation = Quaternion.Euler(0f, 0f, finalAngle);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }

        private float GetAdjustedAngle(float angle)
        {
            float normalized = (angle + 360f) % 360f;

            if (Mathf.Approximately(normalized, 0f) || Mathf.Approximately(normalized, 180f))
                return 45f;

            if (Mathf.Approximately(normalized, 45f))
                return -45f;

            if (Mathf.Approximately(normalized, 90f))
                return 90f;

            return angle;
        }

        public void SetGoPos(Vector3 goPos)
        {
            _goPos = goPos;
        }

    }
}