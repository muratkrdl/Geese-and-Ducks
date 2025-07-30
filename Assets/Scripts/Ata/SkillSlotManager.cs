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

        // Mevcut skill�i ��kar
        SkillBase usedSkill = usedButton.currentSkill;
        activeSkillButtons.Remove(usedButton);

        // Havuzdan kullan�lmayan bir skill bul
        SkillBase newSkill = null;
        foreach (SkillBase prefab in allSkillPrefabs)
        {
            bool alreadyUsed = false;
            foreach (var btn in activeSkillButtons)
            {
                if (btn.currentSkill.skillName == prefab.skillName)
                {
                    alreadyUsed = true;
                    break;
                }
            }

            if (!alreadyUsed && prefab != usedSkill)
            {
                newSkill = prefab;
                break;
            }
        }

        // E�er yeni bir skill bulunduysa de�i�tir
        if (newSkill != null)
        {
            usedButton.AssignSkill(newSkill);
            activeSkillButtons.Add(usedButton);
        }
    }
}
