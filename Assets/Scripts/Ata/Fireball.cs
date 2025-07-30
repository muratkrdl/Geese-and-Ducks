using UnityEngine;

public class Fireball : SkillBase
{
    public GameObject fireballPrefab;
    public Transform centerPoint;

    public override void UseSkill(Vector2 targetPosition)
    {
        if (!ManaManager.instance.UseMana((int)skillCost))
            return;

        Vector2 spawnPos = centerPoint != null ? centerPoint.position : Vector2.zero;

        GameObject go = Instantiate(fireballPrefab, spawnPos, Quaternion.identity);
        go.GetComponent<FireballProjectile>().Initialize(targetPosition, duration, fireballDamage, fireAreaDamage);
    }
}
