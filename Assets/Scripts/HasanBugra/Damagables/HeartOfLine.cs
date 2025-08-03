using Murat.Managers;
using Murat.Utilities;
using UnityEngine;

public class HeartOfLine : MonoBehaviour, IDamageable
{
    [SerializeField] private float health = 200f;

    public void TakeDamage(float damage)
    {
        if (health <= 0) return;

        health -= damage;
        if (health <= 0)
        {
            SfxManager.Instance.PlaySfx(SFXNames.BASE_DESTROY);
            Debug.Log("Heart destroyed!");
        }
    }
}
