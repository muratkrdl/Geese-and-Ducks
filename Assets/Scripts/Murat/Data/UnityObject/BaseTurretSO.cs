using UnityEngine;

namespace Murat.Data.UnityObject
{
    [CreateAssetMenu(fileName = "BaseTurretSO", menuName = "SO/TurretSO")]
    public class BaseTurretSO : ScriptableObject
    {
        public float DetectionRange;
        public float AttackSpeed;
        public float ProjectileSpeed;
        public float ProjectileDamage;

        public float MaxHP;
    }
}