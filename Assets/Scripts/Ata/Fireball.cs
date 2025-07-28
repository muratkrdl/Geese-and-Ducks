using UnityEngine;

public class Fireball : SkillBase
{
    public GameObject fireballPrefab;
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

        GameObject go = Instantiate(fireballPrefab, spawnPos, Quaternion.identity);
        go.GetComponent<FireballProjectile>().Initialize(targetPosition, duration);
    }
}
