using UnityEngine;

[CreateAssetMenu(menuName = "Game/GameManager")]
public class GameManagerSO : ScriptableObject
{
    public int currentLevelIndex;
    public LevelData[] levels;

    public LevelData CurrentLevel =>  levels[currentLevelIndex];
}