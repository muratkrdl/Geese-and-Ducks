using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SkillButton : MonoBehaviour
{
    public SkillBase currentSkill;
    public TextMeshProUGUI skillNameText;

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
        if (skillNameText != null && currentSkill != null)
        {
            skillNameText.text = currentSkill.skillName;
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
