using System.Collections.Generic;
using UnityEngine;

public class SkillSlotManager : MonoBehaviour
{
    public static SkillSlotManager instance;

    public List<SkillButton> activeSkillButtons = new List<SkillButton>();
    public List<SkillBase> allSkillPrefabs = new List<SkillBase>();

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }

    public void ReplaceSkillSlot(SkillButton usedButton)
    {
        if (!activeSkillButtons.Contains(usedButton))
            return;

        int currentIndex = allSkillPrefabs.FindIndex(skill => skill.skillName == usedButton.currentSkill.skillName);
        if (currentIndex == -1)
        {
            return;
        }

        int nextIndex = (currentIndex + 1) % allSkillPrefabs.Count;

        for (int i = 0; i < allSkillPrefabs.Count; i++)
        {
            SkillBase nextSkill = allSkillPrefabs[nextIndex];

            bool isSkillAlreadyActive = activeSkillButtons.Exists(btn => btn != usedButton && btn.currentSkill.skillName == nextSkill.skillName);

            if (!isSkillAlreadyActive)
            {
                usedButton.AssignSkill(nextSkill);
                return;
            }

            nextIndex = (nextIndex + 1) % allSkillPrefabs.Count;
        }
    }


}
