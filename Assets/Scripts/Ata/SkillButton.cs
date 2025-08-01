using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SkillButton : MonoBehaviour
{
    public SkillBase currentSkill;
    public Image iconImage; 

    private Button button;

    void Start()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(OnClick);
        UpdateVisual();
    }

    public void AssignSkill(SkillBase skill)
    {
        currentSkill = skill;
        UpdateVisual();
    }

    void UpdateVisual()
    {
        if (iconImage != null && currentSkill != null)
        {
            iconImage.sprite = currentSkill.skillIcon;
            Debug.Log("Yeni ikon atandý: " + currentSkill.skillName);
        }
    }

    void OnClick()
    {
        if (currentSkill != null)
        {
            SkillManager.instance.SelectSkill(currentSkill);
            SkillManager.instance.lastClickedButton = this;
        }
    }
}

