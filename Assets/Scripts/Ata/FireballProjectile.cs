using UnityEngine;

public class FireballProjectile : MonoBehaviour
{
    private Vector2 targetPos;
    private float duration;
    private float instantDamage;
    private float fireAreaDotDamage;

    public float speed = 10f;
    public GameObject fireAreaPrefab;

    public void Initialize(Vector2 target, float durationFromSkill, float dmg, float dotDamage)
    {
        targetPos = target;
        duration = durationFromSkill;
        instantDamage = dmg;
        fireAreaDotDamage = dotDamage;
    }

    void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, targetPos, speed * Time.deltaTime);

        if (Vector2.Distance(transform.position, targetPos) < 0.1f)
        {
            SpawnFireArea();
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        EnemyBase enemy = other.GetComponent<EnemyBase>();
        if (enemy != null)
        {
            enemy.TakeDamageEnemy(instantDamage); 
            SpawnFireArea();
            Destroy(gameObject);
        }
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
