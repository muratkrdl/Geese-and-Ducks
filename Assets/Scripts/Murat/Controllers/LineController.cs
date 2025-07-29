using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Murat.Controllers
{
    public class LineController : MonoBehaviour
    {
        [SerializeField] private PenController pen;
        [SerializeField] private Gradient defaultGradient;
    
        [SerializeField] private float moveSpeed = .5f;
        [SerializeField] private float reverseMoveSpeed = 1.5f;
        [SerializeField] private Vector2[] coordinates;
        [SerializeField] private LineRenderer myLines;
        [SerializeField] private EdgeCollider2D myCollider;
        
        private int _currentLineIndex;
        private const float GradientMargin = .03f;
        private bool _isReversing;
    
        private void Start()
        {
            myLines.positionCount = 1;
            myLines.SetPosition(0, coordinates[0]);
            _currentLineIndex = 0;
        }

        private void Update()
        {
            MoveLastLine();

            if (Input.GetKeyDown(KeyCode.R))
            {
                _isReversing = !_isReversing;
            }
        }

        private void MoveLastLine()
        {
            if (_currentLineIndex >= coordinates.Length || _currentLineIndex < 0)
                return;

            int targetIndex = _isReversing ? _currentLineIndex - 1 : _currentLineIndex;
            if (targetIndex < 0 || targetIndex >= coordinates.Length)
                return;

            float speed = _isReversing ? reverseMoveSpeed : moveSpeed;
            Vector3 targetPos = coordinates[targetIndex];
            Vector3 currentPos = myLines.GetPosition(_currentLineIndex);
            float step = speed * Time.deltaTime;

            Vector3 newPos = Vector3.MoveTowards(currentPos, targetPos, step);
            myLines.SetPosition(_currentLineIndex, newPos);
            pen.transform.position = newPos;

            if (Vector2.Distance(targetPos, newPos) > 0.01f)
            {
                UpdateCollider();
                return;
            }

            myLines.SetPosition(_currentLineIndex, targetPos);

            if (_isReversing)
                HandleReverse(targetPos);
            else
                HandleForward(targetPos);

            UpdateCollider();
        }

        private void HandleReverse(Vector2 targetPos)
        {
            _currentLineIndex--;

            if (_currentLineIndex >= 0)
            {
                myLines.positionCount--;
                myLines.SetPosition(_currentLineIndex, targetPos);
                if (_currentLineIndex - 1 >= 0)
                    pen.SetGoPos(coordinates[_currentLineIndex - 1]);
            }
        }

        private void HandleForward(Vector2 targetPos)
        {
            _currentLineIndex++;

            if (_currentLineIndex < coordinates.Length)
            {
                myLines.positionCount++;
                myLines.SetPosition(_currentLineIndex, targetPos);

                if (_currentLineIndex + 1 < coordinates.Length)
                    pen.SetGoPos(coordinates[_currentLineIndex + 1]);
            }
        }

        public void StartReverse(int a, int b)
        {
            _isReversing = true;
            UpdateGradientAsync(a,b).Forget();
        }

        private async UniTaskVoid UpdateGradientAsync(int a, int b)
        {
            while (_currentLineIndex != a)
            {
                float startTime = (float)a / (myLines.positionCount - 1);
                float endTime = (float)b / (myLines.positionCount - 1);

                myLines.colorGradient = CreateGradient(startTime, endTime);
                await UniTask.Yield();
            }

            myLines.colorGradient = defaultGradient;
            _isReversing = false;
        }

        private Gradient CreateGradient(float start, float end)
        {
            var gradient = new Gradient();

            var colorKeys = new GradientColorKey[]
            {
                new GradientColorKey(start == 0 ? Color.red : Color.white, 0f),
                new GradientColorKey(start == 0 ? Color.red : Color.white, Mathf.Max(0f, start - GradientMargin)),
                new GradientColorKey(Color.red, start),
                new GradientColorKey(Color.red, end),
                new GradientColorKey(Color.white, Mathf.Min(1f, end + GradientMargin)),
                new GradientColorKey(Color.white, 1f)
            };

            gradient.SetKeys(colorKeys, new[]
            {
                new GradientAlphaKey(1f, 0f),
                new GradientAlphaKey(1f, 1f)
            });

            return gradient;
        }
        
        private void UpdateCollider()
        {
            List<Vector2> colliderPoints = new List<Vector2>(myLines.positionCount);
            for (int i = 0; i < myLines.positionCount; i++)
            {
                Vector3 pos = myLines.GetPosition(i);
                colliderPoints.Add(pos);
            }
            myCollider.SetPoints(colliderPoints);
        }
        
    }
}
