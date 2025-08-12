using UnityEngine;

public abstract class SkillBase : MonoBehaviour
{
    [Header("General Info")]
    public float skillCost;
    public string skillName;
    public float duration;
    public Sprite skillIcon;

    [Header("Fireball")]
    public float fireballDamage;
    public float fireAreaDamage;

    [Header("Freeze")]
    public float freezeDuration;
    public float slowRate;
    public float freezeDamage;

    [Header("Lightning")]
    public float lightningDamage;
    public float lightningRadius;

    [Header("Wind")]
    public float windForce;
    public float windRadius;


    public abstract void UseSkill(Vector2 targetPosition);
    public virtual bool UseSkillWithCheck(Vector2 targetPosition)
    {
        if (!ManaManager.instance.UseMana((int)skillCost))
            return false;

        UseSkill(targetPosition);
        return true;
    }


}
