using Murat.Managers;
using Murat.Utilities;
using UnityEngine;

public class Lightning : SkillBase
{
    public GameObject lightningAreaPrefab;
    public Transform centerPoint;

    public override void UseSkill(Vector2 targetPosition)
    {
        Vector2 spawnPos = centerPoint != null ? centerPoint.position : Vector2.zero;

        GameObject proj = Instantiate(lightningAreaPrefab, spawnPos, Quaternion.identity);

        LightningProjectile projScript = proj.GetComponent<LightningProjectile>();
        if (projScript != null)
        {
            projScript.Initialize(targetPosition, duration, lightningDamage, lightningRadius);
        }
        
        SfxManager.Instance.PlaySfx(SFXNames.THUNDER);
    }

}
