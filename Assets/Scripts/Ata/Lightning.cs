using UnityEngine;

public class Lightning : SkillBase
{
    public GameObject lightningAreaPrefab;
    public Transform centerPoint;

    public override void UseSkill(Vector2 targetPosition)
    {
        if (!ManaManager.instance.UseMana((int)skillCost))
            return;

        Vector2 spawnPos = centerPoint != null ? centerPoint.position : Vector2.zero;

        GameObject proj = Instantiate(lightningAreaPrefab, spawnPos, Quaternion.identity);

        LightningProjectile projScript = proj.GetComponent<LightningProjectile>();
        if (projScript != null)
        {
            projScript.Initialize(targetPosition, duration, lightningDamage, lightningRadius);
        }
    }

}
