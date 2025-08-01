using Murat.Objects;
using Murat.Utilities;

namespace Murat.Systems.ObjectPooling
{
    public class SfxObjectPool : BaseObjectPool<SfxObject>
    {
        public static SfxObjectPool Instance;
        
        protected override void Awake()
        {
            base.Awake();
            Instance = this;
        }

        protected override void OnRelease(SfxObject obj)
        {
            base.OnRelease(obj);
            obj.SetPosition(ConstUtilities.Zero3);
        }
    }
}