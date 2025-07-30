using UnityEngine;
using TMPro;

public class PotionUI : MonoBehaviour
{
    public TextMeshProUGUI potionText;

    void Update()
    {
        if (ManaManager.instance != null)
        {
            potionText.text = "Silgi Tozu: " + ManaManager.instance.currentMana.ToString();
        }
    }
}
