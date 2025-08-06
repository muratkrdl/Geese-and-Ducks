using Murat.Managers;
using Murat.Utilities;
using UnityEngine;

public class Fireball : SkillBase
{
    public GameObject fireballPrefab;
    public GameObject fireTargetMarkerPrefab;
    public Transform centerPoint;

    public override void UseSkill(Vector2 targetPosition)
    {
        if (!ManaManager.instance.UseMana((int)skillCost))
            return;

        Vector2 spawnPos = centerPoint != null ? centerPoint.position : Vector2.zero;

        GameObject marker = Instantiate(fireTargetMarkerPrefab, targetPosition, Quaternion.identity);
        GameObject fireball = Instantiate(fireballPrefab, spawnPos, Quaternion.identity);

        fireball.GetComponent<FireballProjectile>().Initialize(
            targetPosition,
            duration,
            fireballDamage,
            fireAreaDamage,
            marker
        );
        
        SfxManager.Instance.PlaySfx(SFXNames.FIREBALL);
    }
}
