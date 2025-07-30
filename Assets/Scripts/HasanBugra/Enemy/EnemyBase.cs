using UnityEngine;

public enum EnemyType
{
    Normal,
    Ice,
    Fire,
    Metal
}

public class EnemyBase : MonoBehaviour
{
    [Header("Enemy Settings")]
    [SerializeField] private EnemyConfig enemyConfig;
    [SerializeField] private float attackDistance = 1f;
    [SerializeField] private LayerMask damageableLayerMask;
    [SerializeField] private EnemyType enemyType = EnemyType.Normal;
    [SerializeField] private SpriteRenderer Healthfiller;

    //Enemy Proporties
    private float speed = 2f;
    protected float damage = 10f;
    private HeartOfLine heartOfLine = null;
    private float health;
    protected Transform target;
    protected bool isAttacking = false;

    protected virtual void Start()
    {
        speed = enemyConfig.speed;
        damage = enemyConfig.damage;
        health = enemyConfig.maxHealth;
        target = FindAnyObjectByType<HeartOfLine>()?.transform;
    }

    protected virtual void Update()
    {
        if (isAttacking || target == null) return;

        Vector2 direction = (target.position - transform.position).normalized;
        RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, attackDistance, damageableLayerMask);
        if(hit.collider != null)
        {
         heartOfLine = hit.collider.GetComponent<HeartOfLine>();
        }
    
        if (hit.collider != null && hit.collider.gameObject != gameObject)
        {
            IDamageable damageable = hit.collider.GetComponent<IDamageable>();
            if (damageable != null)
            {
                isAttacking = true;
                Attack(damageable,heartOfLine);
                return;
            }
        }

        transform.Translate(direction * speed * Time.deltaTime);
    }

    protected virtual void Attack(IDamageable target ,HeartOfLine heartOfLine)
    {
        target.TakeDamage(damage);
        isAttacking = false;
    }

    public virtual void TakeDamageEnemy(float amount)
    {
        health -= amount;
        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }

    private void OnMouseDown()
    {
        TakeDamageEnemy(1f);
    }

    private void OnDrawGizmosSelected()
    {
        HeartOfLine heart = FindAnyObjectByType<HeartOfLine>();
        if (heart == null) return;

        Gizmos.color = Color.red;
        Vector2 direction = (heart.transform.position - transform.position).normalized;
        Vector3 start = transform.position;
        Vector3 end = start + (Vector3)(direction * attackDistance);

        Gizmos.DrawLine(start, end);
    }
}
