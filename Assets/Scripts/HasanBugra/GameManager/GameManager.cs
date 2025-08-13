using UnityEngine;

public class GameManager : MonoBehaviour
{
    [Header("GamaData")]
    [SerializeField] private GameManagerSO gameManagerData;

    private void Awake()
    {
        Instantiate(gameManagerData.CurrentLevel.heartOfLinePrefab, transform.position, Quaternion.identity);
    }

    public int GetCurrentLevel()
    {
        return gameManagerData.levelOnPlay;
    }
    public int GetLastLevel()
    {
        return gameManagerData.currentLevelIndex;
    }
    public void NextLevel()
    {
        gameManagerData.levelOnPlay++;
    }
    public void OnLevelPast()
    {
        gameManagerData.currentLevelIndex++;
    }
}
