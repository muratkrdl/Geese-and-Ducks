using UnityEngine;

public class FreezeProjectile : MonoBehaviour
{
    private Vector2 targetPos;
    public float speed = 10f;
    public GameObject freezeAreaPrefab;

    private float areaDuration;
    private float freezeTime;
    private float freezeDamage;
    private float slowRate;

    public void Initialize(Vector2 target, float areaDur, float freezeDur, float damage, float slow)
    {
        targetPos = target;
        areaDuration = areaDur;
        freezeTime = freezeDur;
        freezeDamage = damage;
        slowRate = slow;
    }

    void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, targetPos, speed * Time.deltaTime);

        if (Vector2.Distance(transform.position, targetPos) < 0.1f)
        {
            SpawnFreezeArea();
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        EnemyBase enemy = other.GetComponent<EnemyBase>();
        if (enemy != null)
        {
            enemy.TakeDamageEnemy(freezeDamage, EnemyType.Fire);
            enemy.Freeze(freezeTime);
            SpawnFreezeArea();
            Destroy(gameObject);
        }
    }

    private void SpawnFreezeArea()
    {
        if (freezeAreaPrefab != null)
        {
            GameObject area = Instantiate(freezeAreaPrefab, transform.position, Quaternion.identity);
            FreezeArea script = area.GetComponent<FreezeArea>();
            if (script != null)
            {
                script.Initialize(areaDuration, slowRate);
            }
        }
    }
}
