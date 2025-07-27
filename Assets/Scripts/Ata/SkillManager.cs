using UnityEngine;

public class SkillManager : MonoBehaviour
{
    public static SkillManager instance;

    public SkillBase currentSkill;

    private void Awake()
    {
        if (instance == null) instance = this;
    }
    public void SelectSkill(SkillBase skill)
    {
        currentSkill = skill;
        Debug.Log(skill.skillName + " secildi.");
    }

    public void UseSelectedSkill(Vector2 pos)
    {
        if (currentSkill != null)
        {
            currentSkill.UseSkill(pos);
        }
    }
}

