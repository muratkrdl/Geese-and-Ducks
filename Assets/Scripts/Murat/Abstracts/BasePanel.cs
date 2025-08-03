using Murat.Data.UnityObject.CDS;
using Murat.Data.ValueObject;
using Murat.Enums;
using UnityEngine;
using UnityEngine.UI;

namespace Murat.Abstracts
{
    public abstract class BasePanel : MonoBehaviour, IPanelAction
    {
        [SerializeField] protected Button[] buttons;
        [SerializeField] protected PanelTypes panelType;
        
        protected bool _clickedButton = false;
        protected PanelPopUpData _data;
        
        protected virtual void Awake()
        {
            _data = Resources.Load<CD_PANEL>("Data/CDS/CD_PANEL").PanelPopUpData;
        }

        public abstract void OpenPanel();
        
        public abstract void ClosePanel();

        protected void OnClosePanel()
        {
            Destroy(gameObject);
        }
    }
}