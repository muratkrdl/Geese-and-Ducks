using UnityEngine;

public abstract class SkillBase : MonoBehaviour
{
    public float skillCost;
    public string skillName;
    public float duration;
    public float freezeDuration;
    public float fireballDamage;
    public float fireAreaDamage;
    public float slowRate;
    public float freezeDamage;
    public float lightningDamage;
    public float lightningRadius;
   // public float barricadeHp;



    public abstract void UseSkill(Vector2 targetPosition);

}
