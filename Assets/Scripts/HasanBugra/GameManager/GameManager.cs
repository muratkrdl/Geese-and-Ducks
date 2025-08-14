using UnityEngine;
using Murat.Managers; // LineManager için

public class GameManager : MonoBehaviour
{
    [Header("GamaData")]
    [SerializeField] private GameManagerSO gameManagerData;

    private void Awake()
    {
        // Level prefabýný sahneye ekle
        var heartOfLine = Instantiate(
            gameManagerData.CurrentLevel.heartOfLinePrefab,
            transform.position,
            Quaternion.identity
        );

        // LineManager root'ta deðilse bile child'larda bul (inactive dahil)
        var lineManager = heartOfLine.GetComponentInChildren<LineManager>(true);
        if (lineManager == null)
        {
            Debug.LogError("[GameManager] LineManager bulunamadý! Prefab içinde olduðundan emin olun.");
            return;
        }

        // Sahnedeki TÜM SpeedBoostButton'larý bul ve doðru LMC'ye baðla
        // (inactive UI objeleri de dahil ederek güvenli baðlama)
#if UNITY_2023_1_OR_NEWER
        var buttons = FindObjectsByType<SpeedBoostButton>(FindObjectsInactive.Include, FindObjectsSortMode.None);
#else
        var buttons = FindObjectsOfType<SpeedBoostButton>(true); // eski Unity için
#endif
        foreach (var btn in buttons)
        {
            btn.lineController = lineManager.MovementController;
        }
    }

    public int GetCurrentLevel() => gameManagerData.levelOnPlay;
    public int GetLastLevel() => gameManagerData.currentLevelIndex;
    public void NextLevel() => gameManagerData.levelOnPlay++;
    public void OnLevelPast() => gameManagerData.currentLevelIndex++;
}
