using Murat.Managers;
using Murat.Utilities;
using UnityEngine;

namespace Murat.Controllers.Turret
{
    public class TurretPlacementController : MonoBehaviour
    {
        [SerializeField] private GameObject placementPreview;
        [SerializeField] private SpriteRenderer previewRenderer;
        
        [SerializeField] private Color validPlacementColor;
        [SerializeField] private Color invalidPlacementColor;
        
        private bool _isPlacing;
        private bool _canPlace;
        private Vector3 _placementPosition;
        
        private void Start()
        {
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
            
            placementPreview.SetActive(true);
        }
        
        public void CancelPlacement()
        {
            _isPlacing = false;
            placementPreview.SetActive(false);
        }
        
        private void HandlePlacementInput()
        {
            Vector3 mousePosition = ConstUtilities.MainCamera.ScreenToWorldPoint(Input.mousePosition);
            mousePosition.z = 0f;
            
            _canPlace = IsValidPlacement(mousePosition);
            _placementPosition = mousePosition;
            
            placementPreview.transform.position = _placementPosition;
            previewRenderer.color = _canPlace ? validPlacementColor : invalidPlacementColor;
            
            if (Input.GetMouseButtonDown(0) && _canPlace)
            {
                PlaceTurret(_placementPosition);
            }
            else if ((Input.GetMouseButtonDown(0) || Input.GetMouseButtonDown(1)) && !_canPlace)
            {
                CancelPlacement();
            }
        }
        
        private bool IsValidPlacement(Vector3 position)
        {
            Collider2D[] colliders = Physics2D.OverlapCircleAll(position, 0.5f, ConstUtilities.DamagableLayerMask);
            
            return colliders.Length == 0;
        }
        
        private void PlaceTurret(Vector3 position)
        {
            TurretManager.Instance.SpawnTurret(position);
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