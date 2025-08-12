using UnityEngine;
using UnityEngine.UI;

public class SkillButton : MonoBehaviour
{
    public SkillBase currentSkill;
    public Image iconImage;

    [Header("Optional Visuals")]
    public CanvasGroup canvasGroup; // varsa alpha ile grileþtirir

    private Button button;

    void Awake()
    {
        button = GetComponent<Button>();
        if (canvasGroup == null) canvasGroup = GetComponent<CanvasGroup>();
    }

    void OnEnable()
    {
        if (SkillSlotManager.instance != null &&
            !SkillSlotManager.instance.activeSkillButtons.Contains(this))
        {
            SkillSlotManager.instance.activeSkillButtons.Add(this);
        }
    }

    void OnDisable()
    {
        if (SkillSlotManager.instance != null)
        {
            SkillSlotManager.instance.activeSkillButtons.Remove(this);
        }
    }

    void Start()
    {
        button.onClick.AddListener(OnClick);
        UpdateVisual();
        ApplyInteractableState(); // ilk karede doðru olsun
    }

    void Update()
    {
        // Her frame mana yeter mi kontrol et
        ApplyInteractableState();
    }

    public void AssignSkill(SkillBase skill)
    {
        currentSkill = skill;
        UpdateVisual();
        ApplyInteractableState();
    }

    void UpdateVisual()
    {
        if (iconImage != null)
            iconImage.sprite = currentSkill != null ? currentSkill.skillIcon : null;
    }

    void ApplyInteractableState()
    {
        // Güvenli kontroller
        if (ManaManager.instance == null || currentSkill == null)
        {
            SetInteractable(false);
            return;
        }

        // skillCost sende float ise yuvarlayalým; int ise CeilToInt etkisiz:
        int cost = Mathf.CeilToInt(currentSkill.skillCost);
        bool hasEnoughMana = ManaManager.instance.currentMana >= cost;

        SetInteractable(hasEnoughMana);
    }

    void SetInteractable(bool canUse)
    {
        if (button != null) button.interactable = canUse;
        if (canvasGroup != null) canvasGroup.alpha = canUse ? 1f : 0.5f; // görsel feedback
    }

    void OnClick()
    {
        if (currentSkill == null) return;

        SkillManager.instance.SelectSkill(currentSkill);
        SkillManager.instance.lastClickedButton = this;
    }
}
