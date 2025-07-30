using UnityEngine;

public class LightningProjectile : MonoBehaviour
{
    private Vector2 target;
    private float duration;
    private float damage;
    private float radius;

    public float speed = 30f;
    public GameObject lightningAreaPrefab;

    public void Initialize(Vector2 targetPosition, float durationFromSkill, float damageFromSkill, float radiusFromSkill)
    {
        target = targetPosition;
        duration = durationFromSkill;
        damage = damageFromSkill;
        radius = radiusFromSkill;
    }

    void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, target, speed * Time.deltaTime);

        if (Vector2.Distance(transform.position, target) < 0.1f)
        {
            if (lightningAreaPrefab != null)
            {
                GameObject go = Instantiate(lightningAreaPrefab, transform.position, Quaternion.identity);
                LightningArea area = go.GetComponent<LightningArea>();
                if (area != null)
                {
                    area.Initialize(duration, damage, radius); 
                }
            }

            Destroy(gameObject);
        }
    }
}
