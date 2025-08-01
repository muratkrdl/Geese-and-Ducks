using Murat.Abstracts;
using Murat.Enums;
using System.Collections.Generic;
using Murat.Objects;
using Murat.Systems.ObjectPooling;
using UnityEngine;

namespace Murat.Managers
{
    public class TurretManager : MonoBehaviour
    {
        private List<BaseTurret> activeTurrets = new List<BaseTurret>();
        
        public static TurretManager Instance { get; private set; }
        
        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }
            else
            {
                Destroy(gameObject);
            }
        }
        
        public BaseTurret SpawnTurret(Vector3 position)
        {
            BaseTurret turretObject = BasicTurretObjectPool.Instance.GetFromPool();
            turretObject.transform.position = position;
            
            if (turretObject.TryGetComponent<BaseTurret>(out var turret))
            {
                activeTurrets.Add(turret);
            }
            
            return turret;
        }
        
        public void RemoveTurret(BaseTurret turret)
        {
            if (!activeTurrets.Contains(turret)) return;
            
            activeTurrets.Remove(turret);
            if (turret is not BasicTurret)
            {
                Destroy(turret.gameObject);
            }
        }
        
        public List<BaseTurret> GetActiveTurrets()
        {
            return new List<BaseTurret>(activeTurrets);
        }
        
        public int GetActiveTurretCount()
        {
            return activeTurrets.Count;
        }
        
    }
} 