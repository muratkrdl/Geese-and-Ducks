using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{
    [Header("Game Manager")]
    [SerializeField] private GameManagerSO _GameManager;

    [Header("Dot Sprites")]
    [SerializeField] private Sprite greySprite;
    [SerializeField] private Sprite yellowSprite;

    [Header("Dots")]
    [SerializeField] private List<GameObject>  Level1Dots;
    [SerializeField] private List<GameObject>  Level2Dots;
    [SerializeField] private List<GameObject>  Level3Dots;

    private void Start()
    {
        Initilize();
    }

    private void Initilize()
    {
       if(_GameManager.currentLevelIndex >= 1)
        {
            ChangeDotSprites(Level1Dots);
        }
       else if(_GameManager.currentLevelIndex >= 2)
        {
            ChangeDotSprites(Level2Dots);
        }
       else if (_GameManager.currentLevelIndex >= 3)
        {
            ChangeDotSprites(Level3Dots);
        }
    }

    private void ChangeDotSprites(List<GameObject> dotSprites)
    {
        foreach(GameObject dot in dotSprites)
        {
            dot.GetComponent<Image>().sprite = yellowSprite;
        }
    }

    public int CurrtenyLevel => _GameManager.currentLevelIndex;
    
    public void SetLevel(int level)
    {
        _GameManager.levelOnPlay = level;
    }
}
