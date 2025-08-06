using UnityEngine;

public class WindArea : MonoBehaviour
{
    private float radius;
    private float force;
    private float duration;

    [SerializeField] private LayerMask enemyLayer;

    public void Initialize(float radius, float force, float duration)
    {
        this.radius = radius;
        this.force = force;
        this.duration = duration;

        PushEnemies();
        Destroy(gameObject, duration);
    }

    private void PushEnemies()
    {
        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, radius, enemyLayer);

        foreach (Collider2D hit in hits)
        {
            Rigidbody2D rb = hit.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                Vector2 pushDir = (rb.position - (Vector2)transform.position).normalized;
                rb.AddForce(pushDir * force, ForceMode2D.Impulse);
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(transform.position, radius);
    }
}
