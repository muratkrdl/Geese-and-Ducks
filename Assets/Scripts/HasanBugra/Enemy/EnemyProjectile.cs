using UnityEngine;

public class EnemyProjectile : MonoBehaviour
{
    private float projectileDamage;
    [SerializeField] public float lifetime = 3f;
    private void Start()
    {
        Destroy(gameObject, lifetime); // süre dolunca yok et
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        IDamageable damageable = collision.GetComponent<IDamageable>();
        if (damageable != null)
        {
            damageable.TakeDamage(projectileDamage);
            Destroy(gameObject);
        }
    }

    public void SetDamage(float damage)
    {
        projectileDamage = damage;
    }
}
