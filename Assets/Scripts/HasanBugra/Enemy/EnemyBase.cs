using UnityEngine;
using System.Collections;

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
    [SerializeField] private float health = 100f;
    [SerializeField] private float speed = 2f;
    [SerializeField] protected float damage = 10f;
    [SerializeField] private float attackDistance = 1f;
    [SerializeField] private LayerMask damageableLayerMask;
    [SerializeField] private EnemyType enemyType = EnemyType.Normal;

    // Ekleyen: Ata
    private bool isSlowed = false;
    private bool isFrozen = false;
    private Coroutine slowRoutine;
    private Coroutine freezeRoutine;
    [SerializeField] private float baseSpeed = 1f;
    [SerializeField] private int manaReward = 1;



    protected Transform target;
    protected bool isAttacking = false;

    protected virtual void Start()
    {
        speed = baseSpeed;
        target = FindAnyObjectByType<HeartOfLine>()?.transform;
    }

    protected virtual void Update()
    {
        if (isAttacking || isFrozen || target == null) return;

        Vector2 direction = (target.position - transform.position).normalized;
        RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, attackDistance, damageableLayerMask);

        if (hit.collider != null && hit.collider.gameObject != gameObject)
        {
            IDamageable damageable = hit.collider.GetComponent<IDamageable>();
            if (damageable != null)
            {
                isAttacking = true;
                Attack(damageable);
                return;
            }
        }

        transform.Translate(direction * speed * Time.deltaTime);
    }


    protected virtual void Attack(IDamageable target)
    {
        target.TakeDamage(damage);
        isAttacking = false;
    }

    public virtual void TakeDamageEnemy(float amount)
    {
        health -= amount;
        if (health <= 0)
        {
            ManaManager.instance.AddMana(manaReward);
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



    public void TakeDamageOverTime(float totalDamage, float duration)
    {
        StartCoroutine(DamageOverTimeCoroutine(totalDamage, duration));
    }

    private System.Collections.IEnumerator DamageOverTimeCoroutine(float totalDamage, float duration)
    {
        float elapsed = 0f;
        float tickInterval = 1f;   // her 1 saniyede bir hasar
        int totalTicks = Mathf.FloorToInt(duration / tickInterval);
        float damagePerTick = totalDamage / totalTicks;

        while (elapsed < duration)
        {
            TakeDamageEnemy(damagePerTick);
            yield return new WaitForSeconds(tickInterval);
            elapsed += tickInterval;
        }
    }

    public void SlowDown(float slowMultiplier, float slowDuration)
    {
        if (isSlowed)
            return;

        if (slowRoutine != null)
            StopCoroutine(slowRoutine);

        slowRoutine = StartCoroutine(SlowDownCoroutine(slowMultiplier, slowDuration));
    }

    private IEnumerator SlowDownCoroutine(float multiplier, float duration)
    {
        isSlowed = true;
        speed = baseSpeed * multiplier;

        yield return new WaitForSeconds(duration);

        speed = baseSpeed;
        isSlowed = false;
    }



    public void Freeze(float freezeDuration)
    {
        if (isFrozen)
            return;

        if (freezeRoutine != null)
            StopCoroutine(freezeRoutine);

        freezeRoutine = StartCoroutine(FreezeCoroutine(freezeDuration));
    }

    private IEnumerator FreezeCoroutine(float duration)
    {
        isFrozen = true;
        float originalSpeed = speed;
        speed = 0f;

        yield return new WaitForSeconds(duration);

        speed = baseSpeed;
        isFrozen = false;
    }




}
