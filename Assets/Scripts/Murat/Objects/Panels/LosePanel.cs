using DG.Tweening;
using Murat.Abstracts;
using Murat.Utilities;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Murat.Objects.Panels
{
    public class LosePanel : BasePanel
    {
        protected override void Awake()
        {
            base.Awake();
            buttons[0].onClick.AddListener(OnClick_Upgrade);
            buttons[1].onClick.AddListener(OnClick_Restart);
            buttons[2].onClick.AddListener(OnClick_Levels);
        }

        private void OnClick_Upgrade()
        {
            if (_clickedButton) return;
            Debug.Log("Upgrade");
        }

        private void OnClick_Restart()
        {
            if (_clickedButton) return;
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

        private void OnClick_Levels()
        {
            if (_clickedButton) return;
            Debug.Log("Levels");
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