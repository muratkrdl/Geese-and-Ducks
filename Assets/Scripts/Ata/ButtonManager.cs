using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonManager : MonoBehaviour
{
    public void PlayGame()
    {
        SceneManager.LoadScene("LevelSelect");
    }

    public void Level1()
    {
        SceneManager.LoadScene("Level");
    }

    public void StartMenu()
    {
        SceneManager.LoadScene("StartMenu");
    }
}
