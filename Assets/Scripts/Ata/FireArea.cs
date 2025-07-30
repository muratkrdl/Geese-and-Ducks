using UnityEngine;

public class FireArea : MonoBehaviour
{
    [SerializeField] float duration;
    [SerializeField] float damage;

    public void Initialize(float durationFromSkillBase, float dmg)
    {
        duration = durationFromSkillBase;
        damage = dmg;

        Destroy(gameObject, duration);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            if (other.TryGetComponent<EnemyBase>(out var enemy))
            {
                enemy.TakeDamageEnemy(damage);
            }
        }
    }

}
