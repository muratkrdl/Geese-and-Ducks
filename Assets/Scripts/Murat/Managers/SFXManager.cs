using System.Linq;
using Murat.Data.UnityObject;
using Murat.Objects;
using Murat.Systems.ObjectPooling;
using UnityEngine;

namespace Murat.Managers
{
    public class SfxManager : MonoBehaviour
    {
        public static SfxManager Instance;
        
        private SfxDataSo[] _sfx;

        private void Awake()
        {
            Instance = this;
            _sfx = Resources.LoadAll<SfxDataSo>("Data/SFX");
        }

        public void PlaySfx(string sfxName)
        {
            SfxDataSo data = _sfx.FirstOrDefault(item => item.Name == sfxName);
            SfxObject sfxObject = SfxObjectPool.Instance.GetFromPool();
            sfxObject.Play(data);
        }
        
        public void PlaySfx(string sfxName, Vector3 position)
        {
            SfxDataSo data = _sfx.FirstOrDefault(item => item.Name == sfxName);
            SfxObject sfxObject = SfxObjectPool.Instance.GetFromPool();
            sfxObject.SetPosition(position);
            sfxObject.Play(data);
        }
    }
}