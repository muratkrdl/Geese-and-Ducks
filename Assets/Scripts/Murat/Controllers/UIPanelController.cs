using System;
using System.Collections.Generic;
using Murat.Abstracts;
using Murat.Enums;
using Murat.Events;
using UnityEngine;

namespace Murat.Controllers
{
    public class UIPanelController : MonoBehaviour
    {
        [SerializeField] private List<Transform> layers = new();

        private void OnEnable()
        {
            UIEvents.Instance.onOpenPanel += OnOpenPanel;
            UIEvents.Instance.onClosePanel += OnClosePanel;
            UIEvents.Instance.onCloseAllPanels += OnCloseAllPanels;
        }

        public void OnOpenPanel(PanelTypes panelType, int layer)
        {
            OnClosePanel(layer);
            var panel = Instantiate(Resources.Load<GameObject>($"UIPanels/{panelType}Panel"), layers[layer]).GetComponent<BasePanel>();
            panel.OpenPanel();
        }
        
        public void OnClosePanel(int value)
        {
            if (layers[value].childCount <= 0) return;
            var panel = layers[value].GetChild(0).GetComponent<BasePanel>();
            panel.ClosePanel();
        }

        private void OnCloseAllPanels()
        {
            for (int i = 0; i < layers.Count; i++)
            {
                OnClosePanel(i);
            }
        }

        private void OnDisable()
        {
            UIEvents.Instance.onOpenPanel -= OnOpenPanel;
            UIEvents.Instance.onClosePanel -= OnClosePanel;
            UIEvents.Instance.onCloseAllPanels -= OnCloseAllPanels;
        }
    }
}