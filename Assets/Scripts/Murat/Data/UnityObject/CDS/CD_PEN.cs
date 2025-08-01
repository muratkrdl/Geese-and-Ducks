using UnityEngine;
using PenData = Murat.Data.ValueObject.PenData;

namespace Murat.Data.UnityObject.CDS
{
    [CreateAssetMenu(fileName = "CD_PEN", menuName = "Data/CD_PEN", order = 0)]
    public class CD_PEN : ScriptableObject
    {
        public PenData PenData;
    }
}