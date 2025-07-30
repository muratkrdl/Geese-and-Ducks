using UnityEngine;

namespace HasanBugra.Damagables
{
    public class Barrier : MonoBehaviour, IDamageable
    {
        public float health = 5;

        public void TakeDamage(float damage)
        {
            health -= damage;
            if (health <= 0)
            {
                Destroy(gameObject);
            }
        }
    }
}

