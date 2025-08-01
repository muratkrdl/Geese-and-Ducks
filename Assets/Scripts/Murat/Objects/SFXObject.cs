using Cysharp.Threading.Tasks;
using Murat.Data.UnityObject;
using Murat.Systems.ObjectPooling;
using UnityEngine;
using UnityEngine.Pool;

namespace Murat.Objects
{
    public class SfxObject : MonoBehaviour, IPoolableObject<SfxObject>
    {
        private AudioSource _audioSource;
    
        private ObjectPool<SfxObject> _pool;

        public void SetPool(ObjectPool<SfxObject> pool)
        {
            _audioSource = GetComponent<AudioSource>();
            _pool = pool;
        }

        public void Play(SfxDataSo data)
        {
            _audioSource.clip = data.GetClip();
            _audioSource.loop = data.Loop;
            _audioSource.volume = data.Volume;
            _audioSource.pitch = data.GetPitch();
            _audioSource.spatialBlend = data.Is3D;
            _audioSource.maxDistance = data.MaxDistance;
            _audioSource.Play();
            Release().Forget();
        }

        public void SetPosition(Vector3 position)
        {
            transform.position = position;
        }

        private async UniTaskVoid Release()
        {
            await UniTask.WaitUntil(() => !_audioSource.isPlaying);
            _audioSource.Stop();
            ReleasePool();
        }

        public void ReleasePool()
        {
            _pool.Release(this);
        }
    
    }
}