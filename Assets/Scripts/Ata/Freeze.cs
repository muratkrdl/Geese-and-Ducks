using UnityEngine;

public class Freeze : SkillBase
{
    public GameObject freezeProjectilePrefab;
    public Transform centerPoint;

    public override void UseSkill(Vector2 targetPosition)
    {
        if (!ManaManager.instance.UseMana((int)skillCost))
        {
            return;
        }

        Vector2 spawnPos;

        if (centerPoint != null)
        {
            spawnPos = centerPoint.position;
        }
        else
        {
            spawnPos = Vector2.zero;
        }

        GameObject proj = Instantiate(freezeProjectilePrefab, spawnPos, Quaternion.identity);
        proj.GetComponent<FreezeProjectile>().Initialize(targetPosition, duration, freezeDuration);
    }
}
