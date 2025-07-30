using UnityEngine;

public class FireArea : MonoBehaviour
{
    private float duration;
    private float totalDotDamage;

    public void Initialize(float durationFromSkillBase, float dotDamage)
    {
        duration = durationFromSkillBase;
        totalDotDamage = dotDamage;

        Destroy(gameObject, duration);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        EnemyBase enemy = other.GetComponent<EnemyBase>();
        if (enemy != null)
        {
            enemy.TakeDamageOverTime(totalDotDamage, duration);
        }
    }
}
