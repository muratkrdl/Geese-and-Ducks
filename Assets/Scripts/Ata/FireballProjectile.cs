using UnityEngine;

public class FireballProjectile : MonoBehaviour
{
    private Vector2 targetPos;
    private float duration;
    private float instantDamage;
    private float fireAreaDotDamage;

    private GameObject marker;

    public float speed = 10f;
    public GameObject fireAreaPrefab;

    public void Initialize(Vector2 target, float durationFromSkill, float dmg, float dotDamage, GameObject markerObj)
    {
        targetPos = target;
        duration = durationFromSkill;
        instantDamage = dmg;
        fireAreaDotDamage = dotDamage;
        marker = markerObj;
    }

    void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, targetPos, speed * Time.deltaTime);

        Vector2 direction = targetPos - (Vector2)transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, angle + 135f);

        if (Vector2.Distance(transform.position, targetPos) < 0.1f)
        {
            OnHit();
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        EnemyBase enemy = other.GetComponent<EnemyBase>();
        if (enemy != null)
        {
            enemy.TakeDamageEnemy(instantDamage);
            OnHit();
        }
    }

    private void OnHit()
    {
        if (marker != null)
            Destroy(marker);

        SpawnFireArea();
        Destroy(gameObject);
    }

    private void SpawnFireArea()
    {
        if (fireAreaPrefab != null)
        {
            GameObject area = Instantiate(fireAreaPrefab, transform.position, Quaternion.identity);
            FireArea fireAreaScript = area.GetComponent<FireArea>();
            if (fireAreaScript != null)
            {
                fireAreaScript.Initialize(duration, fireAreaDotDamage);
            }
        }
    }
}
