using UnityEngine;
using UnityEngine.UI;

public class PenSpeedBoostButton : MonoBehaviour
{
    [SerializeField] private PenSpeedBoost penSpeedBoostSkill;
    [SerializeField] private Button button;

    private void Reset()
    {
        button = GetComponent<Button>();
    }

    private void Awake()
    {
        if (button == null) button = GetComponent<Button>();
        button.onClick.AddListener(OnClick);
    }

    private void Update()
    {
        if (penSpeedBoostSkill != null && button != null && ManaManager.instance != null)
        {
            button.interactable = ManaManager.instance.currentMana >= (int)penSpeedBoostSkill.skillCost;
        }
    }

    private void OnClick()
    {
        if (penSpeedBoostSkill == null) return;
        penSpeedBoostSkill.UseSkillWithCheck(Vector2.zero); 
    }
}
