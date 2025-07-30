using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [Header("SpawnSettings")]
    [SerializeField] private GameObject[] enemyPrefabs;
    [SerializeField] private float spawnInterval = 2f;
    [SerializeField] private float minSpawnDistance = 15f;
    [SerializeField] private float maxSpawnDistance = 20f;

    private Transform heartOfLine;

    private void Start()
    {
        heartOfLine = FindAnyObjectByType<HeartOfLine>().transform;
        InvokeRepeating(nameof(SpawnEnemy), 1f, spawnInterval);
    }

    void SpawnEnemy()
    {
        Vector2 spawnPos = GetRandomSpawnPosition();
        int index = Random.Range(0, enemyPrefabs.Length);
        Instantiate(enemyPrefabs[index], spawnPos, Quaternion.identity);
    }

    Vector2 GetRandomSpawnPosition()
    {
        float angle = Random.Range(0f, 360f) * Mathf.Deg2Rad;
        Vector2 direction = new Vector2(Mathf.Cos(angle), Mathf.Sin(angle));

        float distance = Random.Range(minSpawnDistance, maxSpawnDistance);

        Vector2 spawnPos = (Vector2)heartOfLine.position + direction * distance;
        return spawnPos;
    }

    private void OnDrawGizmosSelected()
    {
        Transform heart = FindAnyObjectByType<HeartOfLine>().transform;
      
        Gizmos.color = new Color(1f, 1f, 0f, 0.3f); // Sarý, saydam
        Gizmos.DrawWireSphere(heart.position, minSpawnDistance);

        Gizmos.color = new Color(1f, 0f, 0f, 0.3f); // Kýrmýzý, saydam
        Gizmos.DrawWireSphere(heart.position, maxSpawnDistance);
    }
}
