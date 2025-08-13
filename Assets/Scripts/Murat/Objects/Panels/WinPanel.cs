using DG.Tweening;
using Murat.Abstracts;
using Murat.Utilities;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Murat.Objects.Panels
{
    public class WinPanel : BasePanel
    {
        private GameManager gameManager;
        protected override void Awake()
        {
            base.Awake();
            gameManager = FindAnyObjectByType<GameManager>();
            gameManager.OnLevelPast();
            buttons[0].onClick.AddListener(OnClick_Upgrade);
            buttons[1].onClick.AddListener(OnClick_Restart);
            buttons[2].onClick.AddListener(OnClick_NextLevel);
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

        private void OnClick_NextLevel()
        {
            if (_clickedButton) return;
            gameManager.NextLevel();
            SceneManager.LoadScene("Level");
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