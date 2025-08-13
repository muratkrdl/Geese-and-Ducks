using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Murat.Managers;
using Murat.Controllers.Line;

public class SpeedBoostButton : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Button boostButton;
    [SerializeField] public LineMovementController lineController;

    [Header("Cost & Boost")]
    [SerializeField] private int manaCost = 3;
    [SerializeField] private float multiplier = 3f;
    [SerializeField] private float duration = 3f;

    [Header("Grayscale (Siyah-Beyaz)")]
    [SerializeField] private Material grayscaleMat;
    [SerializeField] private bool applyToChildren = true;

    private readonly List<Graphic> _graphics = new List<Graphic>();
    private readonly Dictionary<Graphic, Material> _originalMats = new Dictionary<Graphic, Material>();
    private bool _lastInteractable;

    private void Awake()
    {
        if (boostButton == null) boostButton = GetComponent<Button>();

        if (lineController == null)
        {
            var lm = Object.FindFirstObjectByType<LineManager>();
            if (lm != null) lineController = lm.MovementController;
        }

        // UI graphics topla
        if (boostButton != null)
        {
            if (applyToChildren)
                boostButton.GetComponentsInChildren(true, _graphics);
            else
            {
                var g = boostButton.GetComponent<Graphic>();
                if (g != null) _graphics.Add(g);
            }

            foreach (var g in _graphics)
            {
                if (g != null && !_originalMats.ContainsKey(g))
                    _originalMats[g] = g.material;
            }

            // DisabledColor'ýn soluklaþtýrmasýný kapat
            var colors = boostButton.colors;
            colors.disabledColor = Color.white;
            boostButton.colors = colors;
        }
    }

    private void Start()
    {
        // ÝLK KAREDE görsel durumu uygula
        bool canClick = ComputeCanClick();
        if (boostButton != null) boostButton.interactable = canClick;
        ApplyVisual(canClick);
    }

    private void OnEnable()
    {
        if (boostButton != null) boostButton.onClick.AddListener(OnClickBoost);
    }

    private void OnDisable()
    {
        if (boostButton != null) boostButton.onClick.RemoveListener(OnClickBoost);
    }

    private void Update()
    {
        bool canClick = ComputeCanClick();

        if (boostButton != null) boostButton.interactable = canClick;

        // Yalnýzca deðiþtiðinde materyali güncelle
        if (canClick != _lastInteractable)
            ApplyVisual(canClick);
    }

    private bool ComputeCanClick()
    {
        bool gameOk = (GameStateManager.Instance == null) ||
                      (GameStateManager.Instance.GetCurrentState() == Murat.Enums.GameState.Playing);

        bool hasMana = ManaManager.instance != null &&
                       ManaManager.instance.currentMana >= manaCost;

        bool notReversing = (lineController != null) && !lineController.IsReversing;

        return gameOk &&
               hasMana &&
               (lineController != null) &&
               notReversing &&
               !lineController.IsBoostActive;
    }

    private void ApplyVisual(bool interactableNow)
    {
        _lastInteractable = interactableNow;

        foreach (var g in _graphics)
        {
            if (g == null) continue;

            if (interactableNow)
            {
                if (_originalMats.TryGetValue(g, out var mat))
                    g.material = mat;
            }
            else
            {
                if (grayscaleMat != null)
                    g.material = grayscaleMat;
            }
        }
    }

    private void OnClickBoost()
    {
        if (lineController == null || ManaManager.instance == null) return;
        if (lineController.IsReversing) return;
        if (!ManaManager.instance.UseMana(manaCost)) return;

        lineController.TryStartSpeedBoost(multiplier, duration);
    }
}
