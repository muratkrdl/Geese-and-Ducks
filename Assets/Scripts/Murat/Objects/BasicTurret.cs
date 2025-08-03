using Murat.Abstracts;
using Murat.Managers;
using Murat.Systems.ObjectPooling;
using Murat.Utilities;
using UnityEngine;
using UnityEngine.Pool;

namespace Murat.Objects
{
    public class BasicTurret : BaseTurret, IPoolableObject<BasicTurret>
    {
        [SerializeField] private Transform muzzlePos;

        private ObjectPool<BasicTurret> _pool;
        
        protected override void Attack()
        {
            if (!CurrentTarget) return;
            
            TurretProjectile projectile = TurretProjectileObjectPool.Instance.GetFromPool();
            projectile.Initialize(Data.ProjectileDamage, Data.ProjectileSpeed, CurrentTarget);
            projectile.transform.position = muzzlePos.position;
            
            LastAttackTime = Time.time;
            
            SfxManager.Instance.PlaySfx(SFXNames.TOWER_ATTACK);
        }

        public override void TakeDamage(float damage)
        {
            base.TakeDamage(damage);
            if (CurrentHp <= 0)
            {
                ReleasePool();
            }
        }

        public void SetPool(ObjectPool<BasicTurret> pool)
        {
            _pool = pool;
        }

        public void ReleasePool()
        {
            _pool.Release(this);
        }
    }
} 