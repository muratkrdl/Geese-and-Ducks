using UnityEngine;

public class Lightning : SkillBase
{
    public GameObject lightningProjectilePrefab;
    public Transform centerPoint;

    public override void UseSkill(Vector2 targetPosition)
    {
        if (!ManaManager.instance.UseMana((int)skillCost))
        {
            return;
        }

        Vector2 spawnPos = centerPoint != null ? centerPoint.position : Vector2.zero;

        GameObject projectile = Instantiate(lightningProjectilePrefab, spawnPos, Quaternion.identity);
        projectile.GetComponent<LightningProjectile>().Initialize(targetPosition);
    }
}
