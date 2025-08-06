using Murat.Managers;
using Murat.Utilities;
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
                duration,        // FreezeArea'n�n sahnede kalma s�resi
                freezeDuration,  // Donma s�resi
                freezeDamage,    // FreezeProjectile �arp�nca verece�i hasar
                slowRate         // FreezeArea'n�n yava�latma oran�
            );
            
            SfxManager.Instance.PlaySfx(SFXNames.FREEZE);
        }
    }
}
