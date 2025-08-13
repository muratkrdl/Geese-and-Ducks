using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Murat.Managers;

namespace Murat.Controllers.Turret
{
    public class TurretUIController : MonoBehaviour
    {
        [Header("UI")]
        [SerializeField] private Button basicTurretButton;
        [SerializeField] private TurretPlacementController placementController;

        [Header("Grayscale (Siyah-Beyaz)")]
        [SerializeField] private Material grayscaleMat;   
        [SerializeField] private bool applyToChildren = true; 

        private readonly List<Graphic> _graphics = new List<Graphic>();
        private readonly Dictionary<Graphic, Material> _originalMats = new Dictionary<Graphic, Material>();
        private bool _lastInteractable;

        private void Awake()
        {
            if (basicTurretButton == null)
                basicTurretButton = GetComponentInChildren<Button>(true);

            if (basicTurretButton != null)
            {
                if (applyToChildren)
                    basicTurretButton.GetComponentsInChildren(true, _graphics);
                else
                {
                    var g = basicTurretButton.GetComponent<Graphic>();
                    if (g != null) _graphics.Add(g);
                }

                foreach (var g in _graphics)
                {
                    if (g != null && !_originalMats.ContainsKey(g))
                        _originalMats[g] = g.material;
                }

                var colors = basicTurretButton.colors;
                colors.disabledColor = Color.white;
                basicTurretButton.colors = colors;
            }
        }

        private void Start()
        {
            if (basicTurretButton != null)
                basicTurretButton.onClick.AddListener(StartBasicTurretPlacement);

            ApplyVisual(false);
        }

        private void OnDestroy()
        {
            if (basicTurretButton != null)
                basicTurretButton.onClick.RemoveListener(StartBasicTurretPlacement);
        }

        private void Update()
        {
            bool canAfford = (ManaManager.instance != null &&
                              placementController != null &&
                              ManaManager.instance.currentMana >= placementController.placeCost);

            if (basicTurretButton != null)
                basicTurretButton.interactable = canAfford;

            if (canAfford != _lastInteractable)
                ApplyVisual(canAfford);
        }

        private void ApplyVisual(bool interactableNow)
        {
            _lastInteractable = interactableNow;

            foreach (var g in _graphics)
            {
                if (g == null) continue;

                if (interactableNow)
                {
                    if (_originalMats.TryGetValue(g, out var mat))
                        g.material = mat;
                }
                else
                {
                    if (grayscaleMat != null)
                        g.material = grayscaleMat;
                }
            }
        }

        private void StartBasicTurretPlacement()
        {
            if (placementController != null)
                placementController.StartPlacement();
        }

        public void CancelPlacement()
        {
            if (placementController != null)
                placementController.CancelPlacement();
        }

        public void RemoveAllTurrets()
        {
            var list = TurretManager.Instance.GetActiveTurrets();
            foreach (var turret in list)
            {
                if (turret == null) continue;
                TurretManager.Instance.RemoveTurret(turret);
            }
        }
    }
}
