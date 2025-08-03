using UnityEngine;

public class DeathEffectAutoDestroy : MonoBehaviour
{
    public float destroyDelay = 1.5f;

    void Start()
    {
        Destroy(gameObject, destroyDelay);
    }
}
