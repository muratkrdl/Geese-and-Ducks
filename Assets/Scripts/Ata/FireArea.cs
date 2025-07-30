using UnityEngine;

public class FireArea : MonoBehaviour
{
    private float duration;
    private float damage;

    public void Initialize(float durationFromSkillBase, float dmg)
    {
        duration = durationFromSkillBase;
        damage = dmg;

        Destroy(gameObject, duration);
    }

 /*   private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            Debug.Log("Ateþ alanýna düþman girdi: " + other.name);

            EnemyHealth enemy = other.GetComponent<EnemyHealth>();
            if (enemy != null)
            {
                enemy.TakeDamage(damage);
            }
        }
    }*/
}
