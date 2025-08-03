using System.Net.Mime;
using DG.Tweening;
using Murat.Abstracts;
using Murat.Events;
using Murat.Utilities;
using UnityEngine;

namespace Murat.Objects.Panels
{
    public class PausePanel : BasePanel
    {
        protected override void Awake()
        {
            base.Awake();
            buttons[0].onClick.AddListener(OnClick_Resume);
            buttons[1].onClick.AddListener(OnClick_Quit);
        }

        private void OnClick_Resume()
        {
            if (_clickedButton) return;
            
            _clickedButton = true;
            CoreGameEvents.Instance.OnGameResume?.Invoke();
        }

        private void OnClick_Quit()
        {
            if (_clickedButton) return;
            
            _clickedButton = true;
            
            Application.Quit();
        }
        
        public override void OpenPanel()
        {
            transform.DOScale(ConstUtilities.One3, _data.AnimationDuration).SetEase(_data.AnimationEase);
        }

        public override void ClosePanel()
        {
            transform.DOScale(ConstUtilities.Zero3, _data.AnimationDuration)
                .SetEase(_data.AnimationEase)
                .OnComplete(OnClosePanel);
        }
        
    }
}