using Murat.Managers;
using Murat.Utilities;
using UnityEngine;

public class Wind : SkillBase
{
    public GameObject windAreaPrefab;
    public Transform centerPoint;

    public override void UseSkill(Vector2 targetPosition)
    {
         Vector2 spawnPos = targetPosition;

        Vector2 direction = (targetPosition - (Vector2)centerPoint.position).normalized;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        Quaternion rotation = Quaternion.Euler(0f, 0f, angle + 180f);

        GameObject go = Instantiate(windAreaPrefab, spawnPos, rotation);

        WindArea area = go.GetComponent<WindArea>();
        if (area != null)
        {
            area.Initialize(windRadius, windForce, duration);
        }
        
        SfxManager.Instance.PlaySfx(SFXNames.WIND);
    }
}
