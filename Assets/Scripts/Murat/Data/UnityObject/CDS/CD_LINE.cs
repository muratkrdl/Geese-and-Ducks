using Murat.Data.ValueObject;
using UnityEngine;

namespace Murat.Data.UnityObject.CDS
{
    [CreateAssetMenu(fileName = "CD_LINE", menuName = "Data/CD_LINE", order = 0)]
    public class CD_LINE : ScriptableObject
    {
        public LineMovementData LineMovementData;
    }
}