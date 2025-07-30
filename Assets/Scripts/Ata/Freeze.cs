using UnityEngine;

public class Freeze : SkillBase
{
    public GameObject freezeProjectilePrefab;
    public Transform centerPoint;

    public override void UseSkill(Vector2 targetPosition)
    {
        if (!ManaManager.instance.UseMana((int)skillCost))
            return;

        Vector2 spawnPos = centerPoint != null ? centerPoint.position : Vector2.zero;

        GameObject proj = Instantiate(freezeProjectilePrefab, spawnPos, Quaternion.identity);

        FreezeProjectile freezeProj = proj.GetComponent<FreezeProjectile>();
        if (freezeProj != null)
        {
            freezeProj.Initialize(
                targetPosition,
                duration,        // FreezeArea'nýn sahnede kalma süresi
                freezeDuration,  // Donma süresi
                freezeDamage,    // FreezeProjectile çarpýnca vereceði hasar
                slowRate         // FreezeArea'nýn yavaþlatma oraný
            );
        }
    }
}
