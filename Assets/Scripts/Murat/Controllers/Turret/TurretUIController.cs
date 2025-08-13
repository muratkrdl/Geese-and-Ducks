using Murat.Managers;
using UnityEngine;
using UnityEngine.UI;

namespace Murat.Controllers.Turret
{
    public class TurretUIController : MonoBehaviour
    {
        [SerializeField] private Button basicTurretButton;
        [SerializeField] private TurretPlacementController placementController;

        private void Start()
        {
            basicTurretButton.onClick.AddListener(StartBasicTurretPlacement);
        }

        private void Update()
        {
            if (ManaManager.instance == null || placementController == null)
            {
                basicTurretButton.interactable = false;
                return;
            }

            basicTurretButton.interactable =
                ManaManager.instance.currentMana >= placementController.placeCost;
        }

        private void StartBasicTurretPlacement()
        {
            placementController.StartPlacement();
        }

        public void CancelPlacement()
        {
            placementController.CancelPlacement();
        }

        public void RemoveAllTurrets()
        {
            foreach (var turret in TurretManager.Instance.GetActiveTurrets())
            {
                if (turret == null) continue;
                TurretManager.Instance.RemoveTurret(turret);
            }
        }
    }
}
