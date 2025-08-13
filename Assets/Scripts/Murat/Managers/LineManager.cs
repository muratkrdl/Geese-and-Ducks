using Murat.Abstracts;
using Murat.Controllers;
using Murat.Controllers.Line;
using UnityEngine;

namespace Murat.Managers
{
    public class LineManager : GamePlayBehaviour
    {
        [SerializeField] private PenController pen;
        [SerializeField] private Gradient defaultGradient;

        [SerializeField] private Vector2[] coordinates;
        [SerializeField] private LineRenderer myLines;
        [SerializeField] private EdgeCollider2D myCollider;

        public LineMovementController MovementController => _movementController;

        private LineMovementController _movementController;
        private LineGradientController _gradientController;
        private LineColliderUpdater _colliderUpdater;

        private int _oldReverseA = 999;

        private void Awake()
        {
            _movementController = GetComponent<LineMovementController>();
            if (_movementController == null)
                _movementController = gameObject.AddComponent<LineMovementController>();

            _gradientController = GetComponent<LineGradientController>();
            if (_gradientController == null)
                _gradientController = gameObject.AddComponent<LineGradientController>();

            _colliderUpdater = GetComponent<LineColliderUpdater>();
            if (_colliderUpdater == null)
                _colliderUpdater = gameObject.AddComponent<LineColliderUpdater>();

            _movementController.Initialize(myLines, pen, coordinates);
            _gradientController.Initialize(myLines, defaultGradient);
            _colliderUpdater.Initialize(myLines, myCollider);
        }

        private void Start()
        {
            _movementController.SetCurrentLineIndex(0);
            _movementController.StartLine();
        }

        private void Update()
        {
            _movementController.MoveLastLine();
            _colliderUpdater.UpdateCollider();
        }

        public void StartReverse(int a, int b)
        {
            int realA = Mathf.Min(a, b);
            int realB = Mathf.Max(a, b);
            if (realA <= _oldReverseA)
                _oldReverseA = realA;
            else
                return;

            _gradientController.UpdateGradientAsync
            (
                realA, realB,
                () => _movementController.CurrentLineIndex,
                _movementController.SetReversing,
                () => _oldReverseA = 999
            ).Forget();
        }

        protected override void OnGamePause()
        {
            _movementController.SetStopSpeed();
        }

        protected override void OnGameResume()
        {
            _movementController.SetNormalSpeed();
        }
    }
}
