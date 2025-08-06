using UnityEngine;

public class FreezeArea : MonoBehaviour
{
    private float slowDuration;
    private float slowRate;

    public void Initialize(float duration, float rate)
    {
        slowDuration = duration;
        slowRate = rate;

        Destroy(gameObject, slowDuration);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        EnemyBase enemy = other.GetComponent<EnemyBase>();
        if (enemy != null)
        {
            enemy.SlowDown(slowRate, slowDuration);
        }
    }
}
