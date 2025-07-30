using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class RangedEnemy : EnemyBase
{
    public GameObject projectilePrefab;
    public float shootCooldown = 2f;
    private float lastShootTime;

    protected override void Attack(IDamageable target, HeartOfLine heartOfLine)
    {
        if (Time.time - lastShootTime >= shootCooldown)
        {
            Shoot();
            lastShootTime = Time.time;
        }

        isAttacking = false;
    }

    void Shoot()
    {
        if (projectilePrefab == null || target == null) return;

        GameObject proj = Instantiate(projectilePrefab, transform.position, Quaternion.identity);
        EnemyProjectile enemyProjectile = proj.GetComponent<EnemyProjectile>();
        enemyProjectile.SetDamage(damage);
        enemyProjectile.SetParent(gameObject);
        Rigidbody2D rb = proj.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.linearVelocity = (target.position - transform.position).normalized * 5f;
        }
    }
}
