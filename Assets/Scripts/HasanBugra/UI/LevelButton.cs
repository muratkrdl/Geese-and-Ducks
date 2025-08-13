using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelButton : MonoBehaviour
{
    [Header("Level Index")]
    [SerializeField] private int levelIndex;

    [Header("Button Images")]
    [SerializeField] private Sprite greySprite;
    [SerializeField] private Sprite yellowSprite;

    private Image image;
    private Button levelButton;
    private TextMeshProUGUI textMPRO;
    private LevelManager levelManager;
    private void Start()
    {
        image = GetComponent<Image>();
        levelButton = GetComponent<Button>();
        levelManager = FindAnyObjectByType<LevelManager>(); 
        textMPRO = GetComponentInChildren<TextMeshProUGUI>();
        Initilize();
    }

    private void Initilize()
    {
        textMPRO.text = (levelIndex + 1).ToString();
        if (levelManager.CurrtenyLevel >= levelIndex)
        {
            image.sprite = yellowSprite;
            levelButton.interactable = true;
        }
        else
        {
            image.sprite = greySprite;
            levelButton.interactable = false;
        }
    }
    public void PlayTheLevel()
    {
        levelManager.SetLevel(levelIndex);
        SceneManager.LoadScene("Level");
    }
}
