using Murat.Data.UnityObject.CDS;
using Murat.Data.ValueObject;
using Murat.Enums;
using Murat.Events;
using Murat.Managers;
using System.Collections;
using UnityEngine;

namespace Murat.Controllers.Line
{
    public class LineMovementController : MonoBehaviour
    {
        private LineMovementData _data;
        
        private LineRenderer _myLines;
        private PenController _pen;
        private Vector2[] _coordinates;

        private int _currentLineIndex;
        private bool _isReversing;
        private bool _isLineCompleted;

        private float _realMoveSpeed;

        public int CurrentLineIndex => _currentLineIndex;
        public void SetReversing(bool value) => _isReversing = value;
        public void SetCurrentLineIndex(int value) => _currentLineIndex = value;

        public void Initialize(LineRenderer lineRenderer, PenController penController, Vector2[] coords)
        {
            _myLines = lineRenderer;
            _pen = penController;
            _coordinates = coords;
            _data = Resources.Load<CD_LINE>("Data/CDS/CD_LINE").LineMovementData;
            _realMoveSpeed = _data.MoveSpeed;
        }

        public void StartLine()
        {
            _myLines.positionCount = 1;
            _myLines.SetPosition(0, _coordinates[0]);
            _currentLineIndex = 0;
        }

        public void MoveLastLine()
        {
            if (_isLineCompleted || _currentLineIndex < 0)
                return;

            int targetIndex = _isReversing ? _currentLineIndex - 1 : _currentLineIndex;
            if (targetIndex < 0 || targetIndex >= _coordinates.Length)
                return;

            float speed = _isReversing ? _data.ReverseMoveSpeed : _realMoveSpeed;
            Vector3 targetPos = _coordinates[targetIndex];
            Vector3 currentPos = _myLines.GetPosition(_currentLineIndex);
            float step = speed * Time.deltaTime;

            Vector3 newPos = Vector3.MoveTowards(currentPos, targetPos, step);
            _myLines.SetPosition(_currentLineIndex, newPos);
            _pen.transform.position = newPos;

            if (Vector2.Distance(targetPos, newPos) > 0.01f)
            {
                return;
            }

            _myLines.SetPosition(_currentLineIndex, targetPos);

            if (_isReversing)
                HandleReverse(targetPos);
            else
                HandleForward(targetPos);
        }

        private void HandleReverse(Vector2 targetPos)
        {
            _currentLineIndex--;

            if (_currentLineIndex >= 0)
            {
                _myLines.positionCount--;
                _myLines.SetPosition(_currentLineIndex, targetPos);
                if (_currentLineIndex - 1 >= 0)
                    _pen.SetGoPos(_coordinates[_currentLineIndex - 1]);
            }
        }

        private void HandleForward(Vector2 targetPos)
        {
            _currentLineIndex++;

            if (_currentLineIndex < _coordinates.Length)
            {
                _myLines.positionCount++;
                _myLines.SetPosition(_currentLineIndex, targetPos);

                if (_currentLineIndex + 1 < _coordinates.Length)
                {
                    _pen.SetGoPos(_coordinates[_currentLineIndex + 1]);
                }
            }
            
            if (_currentLineIndex >= _coordinates.Length)
            {
                // Win
                CoreGameEvents.Instance.OnGameWin?.Invoke();
                _isLineCompleted = true;
            }
        }

        public void SetStopSpeed()
        {
            _realMoveSpeed = 0;
        }

        public void SetNormalSpeed()
        {
            _realMoveSpeed = _data.MoveSpeed;
        }

        public IEnumerator SlowDownCoroutine(float multiplier, float duration)
        {
            _realMoveSpeed = _data.MoveSpeed * multiplier;

            yield return new WaitForSeconds(duration);
            yield return new WaitUntil(() => GameStateManager.Instance.GetCurrentState() == GameState.Playing);

            _realMoveSpeed = _data.MoveSpeed; ;

        }
    }
} 