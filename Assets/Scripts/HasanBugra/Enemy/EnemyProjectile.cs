using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class EnemyProjectile : MonoBehaviour
{
    private float projectileDamage;
    private GameObject _parent;
    [SerializeField] public float lifetime = 3f;
    private void Start()
    {
        Destroy(gameObject, lifetime); 
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        IDamageable damageable = collision.GetComponent<IDamageable>();
        if (damageable != null)
        {
            damageable.TakeDamage(projectileDamage);
            ParticleEffectsManager.Instance.PlayHitEffect((collision.transform.position));
            HeartOfLine heartOfLine = collision.GetComponent<HeartOfLine>();
            if (heartOfLine != null) Destroy(_parent);
            Destroy(gameObject);
        }
    }

    public void SetParent(GameObject parent)
    {
        _parent = parent;
    }

    public void SetDamage(float damage)
    {
        projectileDamage = damage;
    }
}
