using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LevelButton : MonoBehaviour
{
    [SerializeField] private int levelIndex;

    private Button levelButton;
    private TextMeshProUGUI textMPRO;
    private LevelManager levelManager;
    private void Start()
    {
        levelButton = GetComponent<Button>();
        levelManager = FindAnyObjectByType<LevelManager>(); 
        textMPRO = GetComponentInChildren<TextMeshProUGUI>();
        textMPRO.text = levelIndex.ToString();

    }

    private void Initilize()
    {
        if(levelManager.CurrtenyLevel >= levelIndex)
        {
            levelButton.interactable = true;
        }
        else
        {
            levelButton.interactable = false;
        }
    }
    public void PlayTheLevel()
    {
        levelManager.SetLevel(levelIndex);
    }
}
