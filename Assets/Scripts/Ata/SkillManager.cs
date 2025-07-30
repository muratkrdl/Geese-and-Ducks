using UnityEngine;

public class SkillManager : MonoBehaviour
{
    public static SkillManager instance;

    public SkillBase currentSkill;
    public SkillButton lastClickedButton;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void SelectSkill(SkillBase skill)
    {
        currentSkill = skill;
    }

    public void UseSelectedSkill(Vector2 pos)
    {
        if (currentSkill != null)
        {
            currentSkill.UseSkill(pos);

            if (lastClickedButton != null)
            {
                SkillSlotManager.instance.ReplaceSkillSlot(lastClickedButton);
                lastClickedButton = null; 
            }

            currentSkill = null;

        }
    }
}
