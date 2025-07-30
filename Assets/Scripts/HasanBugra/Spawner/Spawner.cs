using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [Header("Game Config")]
    [SerializeField] private GameManagerSO gameManager;

    [Header("Spawn Settings")]
    [SerializeField] private float spawnInterval = 2f;

    [Header("Screen Spawn Bounds")]
    [SerializeField] private float screenBuffer = 2f; 

    private Transform heartOfLine;
    private EnemySpawn[] enemySpawns;

    private void Start()
    {
        heartOfLine = FindAnyObjectByType<HeartOfLine>()?.transform;
        enemySpawns = gameManager.CurrentLevel.enemySpawns;
     
        InvokeRepeating(nameof(SpawnEnemy), 1f, spawnInterval);
    }

    void SpawnEnemy()
    {
        GameObject selectedPrefab = GetRandomEnemyByChance();

        Vector2 spawnPos = GetScreenEdgeSpawnPosition();
        Instantiate(selectedPrefab, spawnPos, Quaternion.identity);
    }

    GameObject GetRandomEnemyByChance()
    {
        float totalChance = 0f;

        foreach (var spawn in enemySpawns)
        {
            totalChance += spawn.spawnChance;
        }

        float randomPoint = Random.Range(0f, totalChance);
        float cumulative = 0f;

        foreach (var spawn in enemySpawns)
        {
            cumulative += spawn.spawnChance;
            if (randomPoint <= cumulative)
            {
                return spawn.enemyPrefab;
            }
        }

        return null;
    }

    Vector2 GetScreenEdgeSpawnPosition()
    {
        Camera cam = Camera.main;
        if (cam == null) return Vector2.zero;

        Vector2 screenMin = cam.ViewportToWorldPoint(new Vector2(0, 0));
        Vector2 screenMax = cam.ViewportToWorldPoint(new Vector2(1, 1));

        float left = screenMin.x - screenBuffer;
        float right = screenMax.x + screenBuffer;
        float bottom = screenMin.y - screenBuffer;
        float top = screenMax.y + screenBuffer;

        int side = Random.Range(0, 4);
        Vector2 pos = Vector2.zero;

        switch (side)
        {
            case 0:
                pos = new Vector2(Random.Range(left, right), top);
                break;
            case 1:
                pos = new Vector2(Random.Range(left, right), bottom);
                break;
            case 2:
                pos = new Vector2(left, Random.Range(bottom, top));
                break;
            case 3:
                pos = new Vector2(right, Random.Range(bottom, top));
                break;
        }

        return pos;
    }

    private void OnDrawGizmosSelected()
    {
        Camera cam = Camera.main;
        if (cam == null) return;

        Vector2 screenMin = cam.ViewportToWorldPoint(new Vector2(0, 0));
        Vector2 screenMax = cam.ViewportToWorldPoint(new Vector2(1, 1));

        float left = screenMin.x - screenBuffer;
        float right = screenMax.x + screenBuffer;
        float bottom = screenMin.y - screenBuffer;
        float top = screenMax.y + screenBuffer;

        Gizmos.color = new Color(1f, 0f, 0f, 0.25f);
        Gizmos.DrawLine(new Vector2(left, bottom), new Vector2(right, bottom));
        Gizmos.DrawLine(new Vector2(left, top), new Vector2(right, top));
        Gizmos.DrawLine(new Vector2(left, bottom), new Vector2(left, top));
        Gizmos.DrawLine(new Vector2(right, bottom), new Vector2(right, top));
    }
}
