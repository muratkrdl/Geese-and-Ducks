using Murat.Data.ValueObject;
using UnityEngine;

namespace Murat.Data.UnityObject.CDS
{
    [CreateAssetMenu(fileName = "CD_PANEL", menuName = "Data/CD_PANEL", order = 0)]
    public class CD_PANEL : ScriptableObject
    {
        public PanelPopUpData PanelPopUpData;
    }
}