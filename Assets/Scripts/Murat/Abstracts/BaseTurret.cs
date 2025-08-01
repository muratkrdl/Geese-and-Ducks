using Murat.Data.UnityObject;
using Murat.Enums;
using Murat.Utilities;
using UnityEngine;

namespace Murat.Abstracts
{
    // TODO : Connect it ObjectPoolingSystem
    public abstract class BaseTurret : MonoBehaviour, IDamageable
    {
        public TurretTypes type;

        protected Transform CurrentTarget;
        protected BaseTurretSO Data;
        protected float LastAttackTime;
        protected float CurrentHp;
        
        private Transform _turretHead;
        private bool CanAttack => Time.time - LastAttackTime >= 1f / Data.AttackSpeed;
        
        public virtual void Initialize()
        {
            SetData();
            _turretHead = transform.GetChild(0);
            CurrentHp = Data.MaxHP;
        }

        protected virtual void SetData()
        {
            Data = Resources.Load<BaseTurretSO>("Data/BaseTurretSO");
        }
        
        private void Update()
        {
            if (!CurrentTarget)
            {
                FindTarget();
            }
            else
            {
                if (Vector2.Distance(transform.position, CurrentTarget.position) > Data.DetectionRange)
                {
                    CurrentTarget = null;
                    return;
                }
                
                RotateTurretHead();
                
                if (CanAttack)
                {
                    Attack();
                }
            }
        }
        
        private void FindTarget()
        {
            Collider2D[] enemiesInRange = Physics2D.OverlapCircleAll(transform.position, Data.DetectionRange, ConstUtilities.EnemyLayerMask);
            
            Transform closestEnemy = null;
            float closestDistance = float.MaxValue;
            
            foreach (Collider2D enemy in enemiesInRange)
            {
                if (enemy.CompareTag(ConstUtilities.ENEMY_TAG))
                {
                    float distance = Vector2.Distance(transform.position, enemy.transform.position);
                    if (distance < closestDistance)
                    {
                        closestDistance = distance;
                        closestEnemy = enemy.transform;
                    }
                }
            }
            
            CurrentTarget = closestEnemy;
        }
        
        private void RotateTurretHead()
        {
            if (!_turretHead || !CurrentTarget) return;
            
            Vector2 direction = (CurrentTarget.position - _turretHead.position).normalized;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            _turretHead.rotation = Quaternion.Euler(0, 0, angle);
            
            /* // Smooth Rotation //
             Quaternion targetRotation = Quaternion.Euler(0, 0, angle);
            _turretHead.rotation = Quaternion.RotateTowards
            (
                _turretHead.rotation,
                targetRotation,
                1000f * Time.deltaTime
            );
            */
        }

        protected abstract void Attack();
        
        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(transform.position, Data.DetectionRange);
            
            if (CurrentTarget != null)
            {
                Gizmos.color = Color.red;
                Gizmos.DrawLine(transform.position, CurrentTarget.position);
            }
        }

        public void SetTurretSO(BaseTurretSO turretSo)
        {
            Data = turretSo;
        }
        
        public virtual void TakeDamage(float damage)
        {
            CurrentHp -= damage;
        }

        public void Reset()
        {
            transform.position = ConstUtilities.Zero3;
            CurrentTarget = null;
            Data = null;
            LastAttackTime = 0;
            CurrentHp = 0;
        }
        
    }
}
