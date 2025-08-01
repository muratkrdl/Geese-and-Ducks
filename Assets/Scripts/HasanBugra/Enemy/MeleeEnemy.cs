using System.Collections;
using UnityEngine;

public class MeleeEnemy : EnemyBase
{
    public float knockbackDistance = 1f;
    public float knockbackDuration = 0.1f;

    protected override void Attack(IDamageable target, HeartOfLine heartOfLine) 
    {
        target.TakeDamage(damage);
        ParticleEffectsManager.Instance.PlayHitEffect((target as MonoBehaviour).transform.position);
        Vector2 directionToTarget = (target as MonoBehaviour).transform.position - transform.position;
        Vector2 knockbackDir = -directionToTarget.normalized;

        StartCoroutine(ApplyKnockback(knockbackDir));
        if (heartOfLine != null) Destroy(gameObject);
        isAttacking = false;
    }

    private IEnumerator ApplyKnockback(Vector2 direction)
    {
        float elapsed = 0f;
        Vector3 start = transform.position;
        Vector3 end = start + (Vector3)(direction * knockbackDistance);

        while (elapsed < knockbackDuration)
        {
            transform.position = Vector3.Lerp(start, end, elapsed / knockbackDuration);
            elapsed += Time.deltaTime;
            yield return null;
        }

        transform.position = end;
    }
}