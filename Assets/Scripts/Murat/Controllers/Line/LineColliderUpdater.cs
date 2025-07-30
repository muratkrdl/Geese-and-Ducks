using System.Collections.Generic;
using UnityEngine;

namespace Murat.Controllers.Line
{
    public class LineColliderUpdater : MonoBehaviour
    {
        private LineRenderer _myLines;
        private EdgeCollider2D _myCollider;

        public void Initialize(LineRenderer lineRenderer, EdgeCollider2D edgeCollider)
        {
            _myLines = lineRenderer;
            _myCollider = edgeCollider;
        }

        public void UpdateCollider()
        {
            List<Vector2> colliderPoints = new List<Vector2>(_myLines.positionCount);
            for (int i = 0; i < _myLines.positionCount; i++)
            {
                Vector3 pos = _myLines.GetPosition(i);
                colliderPoints.Add(pos);
            }
            _myCollider.SetPoints(colliderPoints);
        }
    }
} 