using UnityEngine;

public class FireballProjectile : MonoBehaviour
{
    private Vector2 targetPos;
    private float duration; 

    public float speed = 10f; 
    public GameObject fireAreaPrefab; 

    public void Initialize(Vector2 target, float durationFromSkill)
    {
        targetPos = target;
        duration = durationFromSkill;
    }

    void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, targetPos, speed * Time.deltaTime);

        if (Vector2.Distance(transform.position, targetPos) < 0.1f)
        {
            if (fireAreaPrefab != null)
            {
                GameObject area = Instantiate(fireAreaPrefab, targetPos, Quaternion.identity);

                FireArea fireAreaScript = area.GetComponent<FireArea>();
                if (fireAreaScript != null)
                {
                    fireAreaScript.Initialize(duration); 
                }
            }

            Destroy(gameObject);
        }
    }
}
