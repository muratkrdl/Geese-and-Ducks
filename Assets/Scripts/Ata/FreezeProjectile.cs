using UnityEngine;

public class FreezeProjectile : MonoBehaviour
{
    private Vector2 targetPos;
    public float speed = 10f;
    public GameObject freezeAreaPrefab;

    private float areaDuration;
    private float freezeTime;

    public void Initialize(Vector2 target, float d, float freezeDur)
    {
        targetPos = target;
        areaDuration = d;
        freezeTime = freezeDur;
    }

    void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, targetPos, speed * Time.deltaTime);

        if (Vector2.Distance(transform.position, targetPos) < 0.1f)
        {
            if (freezeAreaPrefab != null)
            {
                GameObject area = Instantiate(freezeAreaPrefab, targetPos, Quaternion.identity);
                FreezeArea script = area.GetComponent<FreezeArea>();
                if (script != null)
                {
                    script.Initialize(areaDuration, freezeTime);
                }
            }

            Destroy(gameObject);
        }
    }
}
