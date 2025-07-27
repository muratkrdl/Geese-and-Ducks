using UnityEngine;

public class Freeze : SkillBase
{
    public GameObject freezeProjectilePrefab;
    public Transform centerPoint;

    public override void UseSkill(Vector2 targetPosition)
    {
        if (!ManaManager.instance.UseMana((int)skillCost))
        {
            Debug.Log("Yetersiz mana, freeze atýlamadý.");
            return;
        }

        Vector2 spawnPos = centerPoint != null ? centerPoint.position : Vector2.zero;

        GameObject proj = Instantiate(freezeProjectilePrefab, spawnPos, Quaternion.identity);
        proj.GetComponent<FreezeProjectile>().Initialize(targetPosition, duration, freezeDuration);

        Debug.Log("Freeze fýrlatýldý: " + targetPosition);
    }
}
