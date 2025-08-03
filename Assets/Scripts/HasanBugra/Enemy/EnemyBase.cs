using System;
using System.Collections;
using Murat.Abstracts;
using Murat.Enums;
using Murat.Managers;
using UnityEngine;

[System.Flags]
public enum EnemyType
{
    Normal,
    Fire,
    Ice,
    Metal
}
public class EnemyBase : GamePlayBehaviour
{
    [Header("Enemy Settings")]
    [SerializeField] private EnemyConfig enemyConfig;
    [SerializeField] private float attackDistance = 1f;
    [SerializeField] private LayerMask damageableLayerMask;
    [SerializeField] private EnemyType _enemyType;
    [SerializeField] private SpriteRenderer Healthfiller;

    // Ekleyen: Ata
    private bool isSlowed = false;
    private bool isFrozen = false;
    private Coroutine slowRoutine;
    private Coroutine freezeRoutine;
    [SerializeField] private float baseSpeed = 1f;
    [SerializeField] private int manaReward = 1;
    protected Animator animator;
    protected SpriteRenderer spriteRenderer;


    //Enemy Proporties
    private float speed = 2f;
    protected float damage = 10f;

    private HeartOfLine heartOfLine = null;
    private float health;
    protected Transform target;
    protected bool isAttacking = false;

    private float realSpeed;

    protected virtual void Start()
    {
        speed = enemyConfig.speed;
        damage = enemyConfig.damage;
        health = enemyConfig.maxHealth;
        EnemyType[] types = (EnemyType[])Enum.GetValues(typeof(EnemyType));
        _enemyType = types[UnityEngine.Random.Range(0, types.Length)];
        target = FindAnyObjectByType<HeartOfLine>()?.transform;

        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        realSpeed = speed;
    }

    protected virtual void Update()
    {
        UpdateRunDirection();

        if (isAttacking || target == null) return;

        float distanceToTarget = Vector2.Distance(transform.position, target.position);

        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, attackDistance, damageableLayerMask);

        foreach (var hit in hits)
        {
            IDamageable damageable = hit.GetComponent<IDamageable>();
            if (damageable != null)
            {
                isAttacking = true;

                HeartOfLine heart = hit.GetComponent<HeartOfLine>();
                Attack(damageable, heart);
                return;
            }
        }


        Vector2 direction = (target.position - transform.position).normalized;
        transform.Translate(direction * realSpeed * Time.deltaTime);
    }


    protected virtual void Attack(IDamageable target, HeartOfLine heartOfLine)
    {
        target.TakeDamage(damage);
        isAttacking = false;
    }

    public virtual void TakeDamageEnemy(float amount, EnemyType enemyType)
    {
        if (_enemyType == enemyType)
        {
            amount *= 2;
        }
        else if (enemyType != EnemyType.Normal)
        {
            amount /= 2;
        }


        health -= amount;
        ParticleEffectsManager.Instance.PlayHitEffect(transform.position);
        if (health <= 0)
        {
            ParticleEffectsManager.Instance.PlayDeathEffect(transform.position);
            Destroy(gameObject);
        }
    }

    private void OnMouseDown()
    {
        TakeDamageEnemy(1f, EnemyType.Normal);
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
    private IEnumerator DamageOverTimeCoroutine(float totalDamage, float duration)
    {
        float elapsed = 0f;
        float tickInterval = 1f;   // her 1 saniyede bir hasar
        int totalTicks = Mathf.FloorToInt(duration / tickInterval);
        float damagePerTick = totalDamage / totalTicks;

        while (elapsed < duration)
        {
            TakeDamageEnemy(damagePerTick, EnemyType.Ice);
            yield return new WaitForSeconds(tickInterval);
            yield return new WaitUntil(() => GameStateManager.Instance.GetCurrentState() == GameState.Playing);
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
        realSpeed = baseSpeed * multiplier;

        yield return new WaitForSeconds(duration);
        yield return new WaitUntil(() => GameStateManager.Instance.GetCurrentState() == GameState.Playing);

        realSpeed = baseSpeed;
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
        realSpeed = 0f;

        yield return new WaitForSeconds(duration);
        yield return new WaitUntil(() => GameStateManager.Instance.GetCurrentState() == GameState.Playing);

        realSpeed = baseSpeed;
        isFrozen = false;
    }
    private void UpdateRunDirection()
    {
        if (animator == null || target == null || spriteRenderer == null) return;

        Vector2 toTarget = target.position - transform.position;

        bool isBehind = false;

        if (Mathf.Abs(toTarget.y) >= 0.1f)
        {
            isBehind = toTarget.y > 0;
            animator.SetBool("isRunningBack", isBehind);
            animator.SetBool("isRunningFront", !isBehind);
        }

        if (Mathf.Abs(toTarget.x) >= 0.1f)
        {
            bool lookingLeft = toTarget.x < 0;

            if (isBehind)
            {
                lookingLeft = !lookingLeft;
            }

            spriteRenderer.flipX = lookingLeft;
        }
    }


    protected override void OnGamePause()
    {
        realSpeed = 0;
    }

    protected override void OnGameResume()
    {
        realSpeed = speed;
    }
}