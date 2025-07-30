using UnityEngine;

public class FreezeArea : MonoBehaviour
{
    [SerializeField] float freezeDuration;
    [SerializeField] float freezeAmount;
    [SerializeField] float freezeDamage;

    public void Initialize(float areaDuration, float freezeTime)
    {
        freezeDuration = freezeTime;

        Destroy(gameObject, areaDuration);
    }

     private void OnTriggerEnter2D(Collider2D other)
     {
        if (other.CompareTag("Enemy"))
        {
            if (other.TryGetComponent<EnemyBase>(out var enemy))
            {
                enemy.FreezeEnemy(freezeDuration, freezeAmount);
                enemy.TakeDamageEnemy(freezeDamage);
            }
        }
     } 

}
