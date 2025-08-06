using UnityEngine;

namespace Murat.Data.UnityObject
{
    [CreateAssetMenu(fileName = "PowerUpSlot", menuName = "SO/PowerUp")]
    public class PowerUpSO : ScriptableObject
    {
        public bool Full;
        public Sprite SkillIcon;
        public string SkillName;
        public int Level;
        public int MaxLevel;
        public int Value;
        public int Cost;
        public string Description;
    }
}
