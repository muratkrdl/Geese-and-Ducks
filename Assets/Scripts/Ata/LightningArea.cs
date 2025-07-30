using UnityEngine;

public class LightningArea : MonoBehaviour
{
    private float duration;
    private float damage;
    private float radius;

    [SerializeField] private LayerMask enemyLayer;

    public void Initialize(float durationFromSkill, float damageFromSkill, float radiusFromSkill)
    {
        duration = durationFromSkill;
        damage = damageFromSkill;
        radius = radiusFromSkill;

        DamageEnemiesInRadius();
        Destroy(gameObject, duration);
    }

    private void DamageEnemiesInRadius()
    {
        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, radius, enemyLayer);

        foreach (Collider2D hit in hits)
        {
            EnemyBase enemy = hit.GetComponent<EnemyBase>();
            if (enemy != null)
            {
                enemy.TakeDamageEnemy(damage);
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, radius);
    }
}
