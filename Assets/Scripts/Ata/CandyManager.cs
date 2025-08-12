using UnityEngine;

public class CandyManager : MonoBehaviour
{
    public static CandyManager instance;

    public int currentCandy = 0;

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
        if (CandyUI.instance != null)
        {
            CandyUI.instance.UpdateCandyUI(currentCandy);
        }
    }

    public void AddCandy(int amount)
    {
        currentCandy += amount;

        if (CandyUI.instance != null)
        {
            CandyUI.instance.UpdateCandyUI(currentCandy);
        }
    }
}
