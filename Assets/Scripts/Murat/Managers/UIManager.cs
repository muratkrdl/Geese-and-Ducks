using Murat.Controllers;
using Murat.Enums;
using Murat.Events;
using UnityEngine;

namespace Murat.Managers
{
    public class UIManager : MonoBehaviour
    {
        public static UIManager Instance;
        
        private void Awake()
        {
            Instance = this;
        }

        private void OnEnable()
        {
            SubscribeEvents();
        }

        private void SubscribeEvents()
        {
            CoreGameEvents.Instance.OnGamePause += OnGamePause;
            CoreGameEvents.Instance.OnGameResume += OnGameResume;
        }

        private void OnGamePause()
        {
            UIEvents.Instance.onOpenPanel?.Invoke(PanelTypes.Pause, 2);
        }
        
        private void OnGameResume()
        {
            UIEvents.Instance.onClosePanel?.Invoke(2);
        }

        private void UnSubscribeEvents()
        {
            CoreGameEvents.Instance.OnGamePause -= OnGamePause;
            CoreGameEvents.Instance.OnGameResume -= OnGameResume;
        }
        
        private void OnDisable()
        {
            UnSubscribeEvents();
        }

    }
}