using Murat.Data.UnityObject;
using UnityEngine;

namespace Murat
{
    public class PowerUpSOKeeper : MonoBehaviour
    {
        public static PowerUpSOKeeper Instance;

        [SerializeField] private PowerUpSO[] barrierHp;
        [SerializeField] private PowerUpSO[] fireballAttack;
        [SerializeField] private PowerUpSO[] iceAttack;
        [SerializeField] private PowerUpSO[] lightningAttack;
        [SerializeField] private PowerUpSO[] mainBaseHp;
        [SerializeField] private PowerUpSO[] moneyGain;
        [SerializeField] private PowerUpSO[] towerAttack;
        [SerializeField] private PowerUpSO[] towerSpeed;

        private void Awake()
        {
            Instance = this;
        }

        public PowerUpSO GetPermanentSkillSoByCode(int code, int level)
        {
            return code switch
            {
                0 => barrierHp[level],
                1 => fireballAttack[level],
                2 => iceAttack[level],
                3 => lightningAttack[level],
                4 => mainBaseHp[level],
                5 => moneyGain[level],
                6 => towerAttack[level],
                7 => towerSpeed[level],
                _ => throw new System.NotImplementedException()
            };
        }
    }
}