using TMPro;
using UnityEngine;

public class HealthUI : MonoBehaviour
{
    [SerializeField] private HeartOfLine heartOfLine; 
    [SerializeField] private TextMeshProUGUI healthText;


    private void Start()
    {
        heartOfLine = FindAnyObjectByType<HeartOfLine>();
    }
    void Update()
    {
        if (heartOfLine != null && healthText != null)
        {
            healthText.text = heartOfLine.health.ToString();
        }
    }
}
