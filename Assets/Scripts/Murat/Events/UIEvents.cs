using System;
using Murat.Enums;
using UnityEngine;
using UnityEngine.Events;

namespace Murat.Events
{
    public class UIEvents : MonoBehaviour
    {
        public static UIEvents Instance;

        public UnityAction<PanelTypes, int> onOpenPanel;
        public UnityAction<int> onClosePanel;
        public UnityAction onCloseAllPanels;
        
        private void Awake()
        {
            Instance = this;
        }
    }
}