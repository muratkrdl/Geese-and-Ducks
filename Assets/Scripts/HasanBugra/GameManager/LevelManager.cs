using UnityEngine;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private GameManagerSO _GameManager;

    public int CurrtenyLevel => _GameManager.currentLevelIndex;
    
    public void SetLevel(int level)
    {
        _GameManager.levelOnPlay = level;
    }
}
