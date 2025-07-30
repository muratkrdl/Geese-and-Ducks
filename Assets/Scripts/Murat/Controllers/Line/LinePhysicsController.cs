using System.Linq;
using Murat.Managers;
using Murat.Utilities;
using UnityEngine;

namespace Murat.Controllers.Line
{
    public class LinePhysicsController : MonoBehaviour
    {
        private LineRenderer _lineRenderer;

        private void Awake()
        {
            _lineRenderer = GetComponent<LineRenderer>();
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag(ConstUtilities.ENEMY_TAG))
            {
                Vector3 enemyPos = other.transform.position;

                Vector3[] positions = new Vector3[_lineRenderer.positionCount];
                _lineRenderer.GetPositions(positions);

                var closestIndexes = positions
                    .Select((pos, index) => new { Index = index, Distance = Vector3.Distance(enemyPos, pos) })
                    .OrderBy(p => p.Distance)
                    .Take(2)
                    .Select(p => p.Index)
                    .ToArray();

                GetComponent<LineManager>().StartReverse(closestIndexes[0], closestIndexes[1]);
            }
        }
    }
}
