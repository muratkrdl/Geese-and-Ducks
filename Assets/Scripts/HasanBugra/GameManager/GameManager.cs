using UnityEngine;
using Murat.Managers; // LineManager i�in

public class GameManager : MonoBehaviour
{
    [Header("GamaData")]
    [SerializeField] private GameManagerSO gameManagerData;

    private void Awake()
    {
        // Level prefab�n� sahneye ekle
        var heartOfLine = Instantiate(
            gameManagerData.CurrentLevel.heartOfLinePrefab,
            transform.position,
            Quaternion.identity
        );

        // LineManager root'ta de�ilse bile child'larda bul (inactive dahil)
        var lineManager = heartOfLine.GetComponentInChildren<LineManager>(true);
        if (lineManager == null)
        {
            Debug.LogError("[GameManager] LineManager bulunamad�! Prefab i�inde oldu�undan emin olun.");
            return;
        }

        // Sahnedeki T�M SpeedBoostButton'lar� bul ve do�ru LMC'ye ba�la
        // (inactive UI objeleri de dahil ederek g�venli ba�lama)
#if UNITY_2023_1_OR_NEWER
        var buttons = FindObjectsByType<SpeedBoostButton>(FindObjectsInactive.Include, FindObjectsSortMode.None);
#else
        var buttons = FindObjectsOfType<SpeedBoostButton>(true); // eski Unity i�in
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
