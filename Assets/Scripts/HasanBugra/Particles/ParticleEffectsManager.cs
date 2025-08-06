using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class ParticleEffectsManager : MonoBehaviour
{
    public static ParticleEffectsManager Instance { get; private set; }

    [Header("Particle Prefabs")]
    [SerializeField] private ParticleSystem hitParticles;
    [SerializeField] private int poolSize = 10;

    [SerializeField] private GameObject deathEffectPrefab;

    private Queue<ParticleSystem> pool = new Queue<ParticleSystem>();

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            CreatePool();
        }
        else
            Destroy(gameObject);
    }

    private void CreatePool()
    {
        for (int i = 0; i < poolSize; i++)
        {
            ParticleSystem ps = Instantiate(hitParticles);
            ps.gameObject.SetActive(false);
            pool.Enqueue(ps);
        }
    }

    public void PlayHitEffect(Vector3 position)
    {
        ParticleSystem hitInstance = GetFromPool();
        hitInstance.transform.position = position;
        hitInstance.gameObject.SetActive(true);
        hitInstance.Play();

        StartCoroutine(ReturnToPool(hitInstance));
    }

    private ParticleSystem GetFromPool()
    {
        if (pool.Count > 0)
            return pool.Dequeue();
        else
            return Instantiate(hitParticles);
    }

    private IEnumerator ReturnToPool(ParticleSystem ps)
    {
        yield return new WaitForSeconds(ps.main.duration + ps.main.startLifetime.constantMax);

        ps.gameObject.SetActive(false);
        pool.Enqueue(ps);
    }

    public void PlayDeathEffect(Vector3 position)
    {
        if (deathEffectPrefab == null) return;

        Instantiate(deathEffectPrefab, position, Quaternion.identity);
    }

}