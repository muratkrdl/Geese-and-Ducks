using UnityEngine;

namespace Murat.Utilities
{
    public static class ConstUtilities
    {
        public static readonly Camera MainCamera = Camera.main;

        public const float TurretProjectileLifeTime = 10f;
        
        public static readonly Vector3 Zero3 = Vector3.zero;
        public static readonly Vector3 One3 = Vector3.one;
        
        public static readonly LayerMask EnemyLayerMask = LayerMask.GetMask("Enemy");
        public static readonly LayerMask DamageableLayerMask = LayerMask.GetMask("Damagable");
        
        public const string ENEMY_TAG = "Enemy";
    }
}
