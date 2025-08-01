using DG.Tweening;
using Murat.Data.UnityObject;
using Murat.Managers;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Murat.Objects
{
    public class PowerUpSlot : MonoBehaviour
    {
        [SerializeField] private float animDuration;
        [SerializeField] private Ease animEase;
        
        [SerializeField] private GameObject descriptionPanel;

        [SerializeField] private string playerPrefCode;

        [SerializeField] private int code;

        [SerializeField] private Image iconImage;
        [SerializeField] private TextMeshProUGUI SkillName;
        [SerializeField] private TextMeshProUGUI Description;
        [SerializeField] private TextMeshProUGUI Cost;
    
        [SerializeField] private Image[] levelImages;
    
        private PowerUpSO powerUpSO;
    
        private string playerPrefLevelCode;
    
        private void Start() 
        {
            playerPrefLevelCode = playerPrefCode + "Level";
            GetComponent<Button>().onClick.AddListener(OnClickEvent);
            powerUpSO = PowerUpSOKeeper.Instance.GetPermanentSkillSoByCode(code, PlayerPrefs.GetInt(playerPrefLevelCode));
            SetValues();
        }
        
        private void OnClickEvent()
        {
            if(powerUpSO.Full) return;

            SetDescriptionPanel(descriptionPanel.transform.localScale != Vector3.one);
        }

        private void SetValues()
        {
            iconImage.sprite = powerUpSO.SkillIcon;
            SkillName.text = powerUpSO.SkillName;
    
            for(int i = 0; i < levelImages.Length; i++)
            {
                if(i < powerUpSO.Level)
                    levelImages[i].color = new(0, 0.64f, 0, 1);
            }
    
            if(powerUpSO.Full)
            {
                SetDescriptionPanel(false);
                return;
            }
            PowerUpSO nextSkillSo = PowerUpSOKeeper.Instance.GetPermanentSkillSoByCode(code, PlayerPrefs.GetInt(playerPrefLevelCode) + 1);;
            Description.text = nextSkillSo.Description;
            Cost.text = nextSkillSo.Cost.ToString();
        }

        public void Buy()
        {
            if( /* Bank.Money*/ 999 >=
               PowerUpSOKeeper.Instance.GetPermanentSkillSoByCode(code, PlayerPrefs.GetInt(playerPrefLevelCode) + 1).Cost) 
            {
                powerUpSO = PowerUpSOKeeper.Instance.GetPermanentSkillSoByCode(code, PlayerPrefs.GetInt(playerPrefLevelCode) + 1);
                // Decrease Cost From Bank
                PlayerPrefs.SetInt(playerPrefLevelCode, powerUpSO.Level);
                // Update Money Text
            }
            
            SetValues();
            SetDescriptionPanel(false);
        }
    
        private void SetDescriptionPanel(bool value)
        {
            float targetValue = value ? 1f : 0f;
            descriptionPanel.transform.DOScale(targetValue, animDuration).SetEase(animEase);
        }
    }
}
