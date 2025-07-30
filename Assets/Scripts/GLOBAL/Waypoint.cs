using UnityEngine;

namespace GLOBAL
{
    public class Waypoint : MonoBehaviour
    {
        [SerializeField] private Vector3[] points;
        public Vector3[] Points => points;
        public Vector3 CurrentPosition => _currentPosition;

        private Vector3 _currentPosition;
        private bool _gameStarted = false;

        private void Start()
        {
            _gameStarted = true;
            _currentPosition = transform.position;    
        }

        public Vector3 GetWaypointPosition(int index)
        {
            return CurrentPosition + Points[index];
        }
        private void OnDrawGizmos()
        {
            if (!_gameStarted && transform.hasChanged)
            {
                _currentPosition = transform.position;
            }
            for (int i = 0; i < points.Length; i++) 
            {
                Gizmos.color = Color.yellow;
                Gizmos.DrawWireSphere(points[i] + _currentPosition, 0.15f);

                if(i < points.Length - 1) 
                {
                    Gizmos.color = Color.gray;
                    Gizmos.DrawLine(points[i] + _currentPosition, points[i + 1] + _currentPosition);  
                }
            }
        }
    }
}