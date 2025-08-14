using Murat.Systems.ObjectPooling;
using Murat.Utilities;
using UnityEngine;
using UnityEngine.Pool;

namespace Murat.Objects
{
    public class TurretProjectile : MonoBehaviour, IPoolableObject<TurretProjectile>
    {
        private float _damage;
        private float _speed;
        private Transform _target;
        private ObjectPool<TurretProjectile> _pool;

        public enum ShortAxis { Right, Up }
        [Header("Orientation")]
        [SerializeField] private ShortAxis shortAxis = ShortAxis.Right;
        [SerializeField] private bool lockTo2DPlane = true;

        public void SetPool(ObjectPool<TurretProjectile> pool) => _pool = pool;
        public void ReleasePool() => _pool.Release(this);

        private void Update()
        {
            if (!_target)
            {
                ReleasePool();
                return;
            }

            Vector2 direction = (_target.position - transform.position).normalized;

            if (shortAxis == ShortAxis.Right)
                transform.right = direction;   
            else
                transform.up = direction;     

            if (lockTo2DPlane)
            {
                var e = transform.eulerAngles;
                e.x = 0f; e.y = 0f;
                transform.eulerAngles = e;
            }

            transform.position += (Vector3)(direction * (_speed * Time.deltaTime));
        }

        public void Initialize(float projectileDamage, float projectileSpeed, Transform targetTransform)
        {
            _damage = projectileDamage;
            _speed = projectileSpeed;
            _target = targetTransform;
        }

        public void Reset()
        {
            _target = null;
            transform.position = ConstUtilities.Zero3;
            _speed = 0;
            _damage = 0;
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag(ConstUtilities.ENEMY_TAG))
            {
                if (other.TryGetComponent<EnemyBase>(out var enemy))
                {
                    enemy.TakeDamageEnemy(_damage, EnemyType.Normal);
                    ReleasePool();
                }
            }
        }
    }
}
