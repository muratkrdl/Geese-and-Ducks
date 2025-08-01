using Murat.Abstracts;
using Murat.Managers;
using Murat.Objects;

namespace Murat.Systems.ObjectPooling
{
    public class BasicTurretObjectPool : BaseObjectPool<BasicTurret>
    {
        public static BasicTurretObjectPool Instance;

        protected override void Awake()
        {
            base.Awake();
            Instance = this;
        }

        protected override void OnGet(BasicTurret obj)
        {
            base.OnGet(obj);
            obj.Initialize();
        }

        protected override void OnRelease(BasicTurret obj)
        {
            base.OnRelease(obj);
            TurretManager.Instance.RemoveTurret(obj);
            obj.Reset();
        }
    }
}