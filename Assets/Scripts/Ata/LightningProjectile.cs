using UnityEngine;

public class LightningProjectile : MonoBehaviour
{
    private Vector2 target;
    public float speed = 30f;
    public GameObject lightningAreaPrefab;

    public void Initialize(Vector2 targetPosition)
    {
        target = targetPosition;
    }

    void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, target, speed * Time.deltaTime);

        if (Vector2.Distance(transform.position, target) < 0.1f)
        {
            if (lightningAreaPrefab != null)
            {
                Instantiate(lightningAreaPrefab, transform.position, Quaternion.identity);
            }

            Destroy(gameObject);
        }
    }
}
