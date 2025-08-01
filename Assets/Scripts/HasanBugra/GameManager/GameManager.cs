using UnityEngine;

public class GameManager : MonoBehaviour
{
    [Header("GamaData")]
    [SerializeField] private GameManagerSO gameManagerData;

    private void Start()
    {
        Instantiate(gameManagerData.CurrentLevel.heartOfLinePrefab, transform.position, Quaternion.identity);
    }

    public void OnLevelPast()
    {
        gameManagerData.currentLevelIndex++;
    }
}
