using UnityEngine;

public class ManaManager : MonoBehaviour
{
    public static ManaManager instance;

    public int currentMana = 0;
    public int maxMana = 10;

    public float secForMana = 3f; 
    private float timer = 0f;

    void Awake()
    {
        if (instance == null) instance = this;
    }

    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= secForMana && currentMana < maxMana)
        {
            currentMana++;
            timer = 0f;
            Debug.Log("Silgi Tozu eklendi. Toplam: " + currentMana);
        }
    }
    public bool UseMana(int amount)
    {
        if (currentMana >= amount)
        {
            currentMana -= amount;
            Debug.Log("Silgi Tozu kullanýldý. Kalan: " + currentMana);
            return true;
        }
        else
        {
            Debug.Log("Yetersiz Silgi Tozu!");
            return false;
        }
    }
}
