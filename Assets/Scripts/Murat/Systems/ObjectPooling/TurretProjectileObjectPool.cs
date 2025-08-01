using Murat.Abstracts;

namespace Murat.Systems.ObjectPooling
{
    public class TurretProjectileObjectPool : BaseObjectPool<TurretProjectile>
    {
        public static TurretProjectileObjectPool Instance;

        protected override void Awake()
        {
            base.Awake();
            Instance = this;
        }

        protected override void OnRelease(TurretProjectile obj)
        {
            base.OnRelease(obj);
            obj.Reset();
        }
    }
}