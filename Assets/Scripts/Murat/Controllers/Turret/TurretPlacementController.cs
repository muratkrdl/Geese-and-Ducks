using Murat.Managers;
using Murat.Utilities;
using UnityEngine;

namespace Murat.Controllers.Turret
{
    public class TurretPlacementController : MonoBehaviour
    {
        [Header("Preview")]
        [SerializeField] private GameObject placementPreview;
        [SerializeField] private SpriteRenderer previewRenderer;

        [SerializeField] private Color validPlacementColor = Color.white;
        [SerializeField] private Color invalidPlacementColor = Color.red;

        [Header("Cost")]
        public int placeCost = 5; 

        private bool _isPlacing;
        private bool _canPlace;
        private Vector3 _placementPosition;

        private void Start()
        {
            if (previewRenderer == null && placementPreview != null)
                previewRenderer = placementPreview.GetComponent<SpriteRenderer>();
        }

        private void Update()
        {
            if (!_isPlacing) return;
            HandlePlacementInput();
        }

        public void StartPlacement()
        {
            _isPlacing = true;
            if (placementPreview != null) placementPreview.SetActive(true);
        }

        public void CancelPlacement()
        {
            _isPlacing = false;
            if (placementPreview != null) placementPreview.SetActive(false);
        }

        private void HandlePlacementInput()
        {
            Vector3 mousePosition = ConstUtilities.MainCamera.ScreenToWorldPoint(Input.mousePosition);
            mousePosition.z = 0f;

            _canPlace = IsValidPlacement(mousePosition);
            _placementPosition = mousePosition;

            bool canAfford = ManaManager.instance != null &&
                             ManaManager.instance.currentMana >= placeCost;

            if (placementPreview != null)
            {
                placementPreview.transform.position = _placementPosition;
                if (previewRenderer != null)
                    previewRenderer.color = (_canPlace && canAfford) ? validPlacementColor : invalidPlacementColor;
            }

            if (Input.GetMouseButtonDown(0))
            {
                if (_canPlace && canAfford)
                {
                    PlaceTurret(_placementPosition);
                }
            }
            else if (Input.GetMouseButtonDown(1))
            {
                CancelPlacement();
            }
        }

        private bool IsValidPlacement(Vector3 position)
        {
            Collider2D[] colliders = Physics2D.OverlapCircleAll(position, 0.5f, ConstUtilities.DamageableLayerMask);
            return colliders.Length == 0;
        }

        private void PlaceTurret(Vector3 position)
        {
            if (ManaManager.instance != null && ManaManager.instance.UseMana(placeCost))
            {
                TurretManager.Instance.SpawnTurret(position);
            }

            CancelPlacement();
        }

        private void OnDrawGizmos()
        {
            if (!_isPlacing) return;
            Gizmos.color = _canPlace ? validPlacementColor : invalidPlacementColor;
            Gizmos.DrawWireSphere(_placementPosition, 0.5f);
        }
    }
}
