using TMPro;
using UnityEngine;

public class CandyUI : MonoBehaviour
{
    public static CandyUI instance;

    [SerializeField] private TextMeshProUGUI candyText;

    void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
    }

    void Start()
    {
        int initial = (CandyManager.instance != null) ? CandyManager.instance.currentCandy : 0;
        UpdateCandyUI(initial);
    }

    public void UpdateCandyUI(int amount)
    {
        if (candyText != null)
        {
            candyText.text = amount.ToString();
        }
    }
}
