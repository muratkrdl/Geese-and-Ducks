using UnityEngine;

public abstract class SkillBase : MonoBehaviour
{
    public float skillCost;
    public string skillName;
    public float duration;
    public float freezeDuration;


    public abstract void UseSkill(Vector2 targetPosition);

}
