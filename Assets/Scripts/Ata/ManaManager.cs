using Murat.Enums;
using Murat.Managers;
using UnityEngine;

public class ManaManager : MonoBehaviour
{
    public static ManaManager instance;

    public int currentMana = 1;
    public int maxMana = 10;

    public float secForMana = 3f;
    private float timer = 0f;

    void Awake()
    {
        if (instance == null) instance = this;
    }

    void Update()
    {
        if (GameStateManager.Instance.GetCurrentState() != GameState.Playing) return;
        
        timer += Time.deltaTime;

        if (timer >= secForMana && currentMana < maxMana)
        {
            currentMana++;
            timer = 0f;
        }
    }
    public bool UseMana(int amount)
    {
        if (currentMana >= amount)
        {
            currentMana -= amount;
            return true;
        }
        else
        {
            return false;
        }
    }
    public void AddMana(int amount)
    {
        currentMana = Mathf.Min(currentMana + amount, maxMana);
    }

}
