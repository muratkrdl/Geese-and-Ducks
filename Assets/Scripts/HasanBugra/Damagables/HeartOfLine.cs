using UnityEngine;

public class HeartOfLine : MonoBehaviour, IDamageable
{
    [SerializeField] private float health = 200f;

    public void TakeDamage(float damage)
    {
        health -= damage;
        if (health <= 0)
        {
            Debug.Log("Heart destroyed!");
        }
    }
}
