using DG.Tweening;
using Murat.Data.UnityObject;
using Murat.Data.UnityObject.CDS;
using Murat.Data.ValueObject;
using Murat.Managers;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Murat.Objects
{
    public class PowerUpSlot : MonoBehaviour
    {
        [SerializeField] private GameObject descriptionPanel;

        [SerializeField] private string playerPrefCode;

        [SerializeField] private int code;

        [SerializeField] private Image iconImage;
        [SerializeField] private TextMeshProUGUI description;
        [SerializeField] private TextMeshProUGUI cost;
    
        [SerializeField] private Image[] levelImages;
    
        private PowerUpSO _powerUpSo;
        private PanelPopUpData _data;
        private string _playerPrefLevelCode;
    
        private void Start() 
        {
            _playerPrefLevelCode = playerPrefCode + "Level";
            _data = Resources.Load<CD_PANEL>("Data/CDS/CD_PANEL").PanelPopUpData;
            _powerUpSo = PowerUpSOKeeper.Instance.GetPermanentSkillSoByCode(code, PlayerPrefs.GetInt(_playerPrefLevelCode));
            GetComponent<Button>().onClick.AddListener(OnClickEvent);
            SetValues();
        }
        
        private void OnClickEvent()
        {
            if(_powerUpSo.Full) return;

            SetDescriptionPanel(descriptionPanel.transform.localScale != Vector3.one);
        }

        private void SetValues()
        {
            iconImage.sprite = _powerUpSo.SkillIcon;
    
            for(int i = 0; i < levelImages.Length; i++)
            {
                if(i < _powerUpSo.Level)
                    levelImages[i].color = new(0, 0.64f, 0, 1);
            }
    
            if(_powerUpSo.Full)
            {
                SetDescriptionPanel(false);
                return;
            }
            PowerUpSO nextSkillSo = PowerUpSOKeeper.Instance.GetPermanentSkillSoByCode(code, PlayerPrefs.GetInt(_playerPrefLevelCode) + 1);;
            description.text = nextSkillSo.Description;
            cost.text = nextSkillSo.Cost.ToString();
        }

        public void Buy()
        {
            if( /* Bank.Money*/ 999 >=
               PowerUpSOKeeper.Instance.GetPermanentSkillSoByCode(code, PlayerPrefs.GetInt(_playerPrefLevelCode) + 1).Cost) 
            {
                _powerUpSo = PowerUpSOKeeper.Instance.GetPermanentSkillSoByCode(code, PlayerPrefs.GetInt(_playerPrefLevelCode) + 1);
                // Decrease Cost From Bank
                PlayerPrefs.SetInt(_playerPrefLevelCode, _powerUpSo.Level);
                // Update Money Text
            }
            
            SetValues();
            SetDescriptionPanel(false);
        }
    
        private void SetDescriptionPanel(bool value)
        {
            float targetValue = value ? 1f : 0f;
            descriptionPanel.transform.DOScale(targetValue, _data.AnimationDuration).SetEase(_data.AnimationEase);
        }
    }
}
