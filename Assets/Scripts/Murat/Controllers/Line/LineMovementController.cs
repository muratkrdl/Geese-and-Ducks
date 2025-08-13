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

        [Header("Debug")]
        [SerializeField] private float _debugSpeedUsed;   // gözlem
        [SerializeField] private float _realMoveSpeed;    // anlık hız (gözlem)

        private LineRenderer _myLines;
        private PenController _pen;
        private Vector2[] _coordinates;

        private int _currentLineIndex;
        private bool _isReversing;
        private bool _isLineCompleted;

        private float _baseMoveSpeed;   

        private Coroutine _boostCR;
        public bool IsBoostActive => _boostCR != null;
        public bool IsReversing => _isReversing;

        public float BaseSpeed => _baseMoveSpeed;
        public int CurrentLineIndex => _currentLineIndex;

        public void SetReversing(bool value) => _isReversing = value;
        public void SetCurrentLineIndex(int value) => _currentLineIndex = value;

        private void Awake()
        {
            if (_baseMoveSpeed <= 0f)
            {
                var cd = Resources.Load<CD_LINE>("Data/CDS/CD_LINE");
                if (cd != null)
                {
                    _data = cd.LineMovementData;
                    _baseMoveSpeed = _data.MoveSpeed;
                    _realMoveSpeed = _baseMoveSpeed > 0f ? _baseMoveSpeed : 0.25f;
                }
                else
                {
                    _baseMoveSpeed = 0.25f;
                    _realMoveSpeed = _baseMoveSpeed;
                }
            }
        }

        public void Initialize(LineRenderer lineRenderer, PenController penController, Vector2[] coords)
        {
            _myLines = lineRenderer;
            _pen = penController;
            _coordinates = coords;

            var cd = Resources.Load<CD_LINE>("Data/CDS/CD_LINE");
            if (cd != null)
            {
                _data = cd.LineMovementData;
                _baseMoveSpeed = _data.MoveSpeed;
                _realMoveSpeed = _baseMoveSpeed;
            }
        }

        public void StartLine()
        {
            _myLines.positionCount = 1;
            _myLines.SetPosition(0, _coordinates[0]);
            _currentLineIndex = 0;
        }

        public void MoveLastLine()
        {

            if (_isLineCompleted || _currentLineIndex < 0) return;

            int targetIndex = _isReversing ? _currentLineIndex - 1 : _currentLineIndex;
            if (targetIndex < 0 || targetIndex >= _coordinates.Length) return;

            float speed = _isReversing ? _data.ReverseMoveSpeed : _realMoveSpeed;
            _debugSpeedUsed = speed;

            Vector3 targetPos = _coordinates[targetIndex];
            Vector3 currentPos = _myLines.GetPosition(_currentLineIndex);
            float step = speed * Time.deltaTime;

            Vector3 newPos = Vector3.MoveTowards(currentPos, targetPos, step);
            _myLines.SetPosition(_currentLineIndex, newPos);
            _pen.transform.position = newPos;

            if (Vector2.Distance(targetPos, newPos) > 0.01f) return;

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
                    _pen.SetGoPos(_coordinates[_currentLineIndex + 1]);
            }

            if (_currentLineIndex >= _coordinates.Length)
            {
                CoreGameEvents.Instance.OnGameWin?.Invoke();
                _isLineCompleted = true;
            }
        }

        public void SetStopSpeed(bool force = false)
        {
            if (!force && IsBoostActive) return; // Boost varken dokunma
            _realMoveSpeed = 0f;
        }

        public void SetNormalSpeed(bool force = false)
        {
            if (!force && IsBoostActive) return; // Boost varken dokunma
            _realMoveSpeed = _baseMoveSpeed;
        }

        /// <summary>
        /// Boost: süre boyunca hız = BaseSpeed * multiplier (sadece ileri yönde).
        /// Reverse iken boost başlatılmaz.
        /// </summary>
        public bool TryStartSpeedBoost(float multiplier, float duration)
        {
            if (IsBoostActive) return false;
            if (_isReversing) return false;        // reverse iken devre dışı
            if (multiplier <= 1f || duration <= 0f) return false;

            _boostCR = StartCoroutine(SpeedBoostCoroutine(multiplier, duration));
            return true;
        }

        private IEnumerator SpeedBoostCoroutine(float multiplier, float duration)
        {
            _realMoveSpeed = _baseMoveSpeed * multiplier;
            // Debug.Log($"[LMC] BOOST ON: real={_realMoveSpeed}");

            yield return new WaitForSeconds(duration);

            if (GameStateManager.Instance != null)
                yield return new WaitUntil(() => GameStateManager.Instance.GetCurrentState() == GameState.Playing);

            _realMoveSpeed = _baseMoveSpeed;
            // Debug.Log($"[LMC] BOOST OFF: real={_realMoveSpeed}");
            _boostCR = null;
        }
    }
}
