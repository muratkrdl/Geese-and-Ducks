using Murat.Events;
using Murat.Managers;
using Murat.Utilities;
using UnityEngine;

public class HeartOfLine : MonoBehaviour, IDamageable
{
    public float health = 200f;

    public void TakeDamage(float damage)
    {
        if (health <= 0) return;

        health -= damage;
        if (health <= 0)
        {
            // Lose
            SfxManager.Instance.PlaySfx(SFXNames.BASE_DESTROY);
            CoreGameEvents.Instance.OnGameLose?.Invoke();
            Debug.Log("Heart destroyed!");
        }
    }
}
