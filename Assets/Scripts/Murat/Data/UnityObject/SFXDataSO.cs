using UnityEngine;

namespace Murat.Data.UnityObject
{
    [CreateAssetMenu(fileName = "New SFXData", menuName = "SO/SFXData")]
    public class SfxDataSo : ScriptableObject
    {
        public string Name;
        
        public AudioClip[] Clips;
        
        public bool Loop;
        
        [Range(0,1)] public float Volume = .5f;
        [Range(-3,3)] public float MinPitch = 1f;
        [Range(-3,3)] public float MaxPitch = 1f;
        [Range(0,1)] public float Is3D = 0f;
        
        public float MaxDistance = 500f;

        public AudioClip GetClip()
        {
            return Clips[Random.Range(0, Clips.Length)];
        }

        public float GetPitch()
        {
            return Random.Range(MinPitch, MaxPitch);
        }
    }
}