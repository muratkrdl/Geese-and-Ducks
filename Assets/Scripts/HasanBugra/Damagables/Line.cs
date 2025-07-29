using UnityEngine;

public class Line : MonoBehaviour, IDamageable
{
    [SerializeField] private float health = 20f;

    public void TakeDamage(float damage)
    {
        health -= damage;
        if (health <= 0)
        {
            Debug.Log("Line destroyed!");
        }
    }
}
