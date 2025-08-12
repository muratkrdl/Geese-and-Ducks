using UnityEngine;

public class Barricade : SkillBase
{
    public GameObject barricadePrefab;
    public Transform centerPoint;

    public override void UseSkill(Vector2 targetPosition)
    {


        // Merkezden hedefe doğru yön
        Vector2 direction = targetPosition - (Vector2)centerPoint.position;

        // Barikat bu yöne dönük olacak
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        Quaternion rotation = Quaternion.Euler(0f, 0f, angle + 270f);
        GameObject obj = Instantiate(barricadePrefab, targetPosition, rotation);
    }
}


