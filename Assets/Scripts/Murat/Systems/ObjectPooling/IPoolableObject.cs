using UnityEngine.Pool;

namespace Murat.Systems.ObjectPooling
{
    public interface IPoolableObject<T> where T : class
    {
        void SetPool(ObjectPool<T> pool);
        void ReleasePool();
    }
}