using Murat.Enums;
using Murat.Events;
using UnityEngine;

namespace Murat.Managers
{
    public class GameStateManager : MonoBehaviour
    {
        public static GameStateManager Instance;
        
        private GameState _currentState;
        
        private void Awake()
        {
            Instance = this;
            _currentState = GameState.Playing;
        }

        private void OnEnable()
        {
            SubscribeEvents();
        }

        private void SubscribeEvents()
        {
            CoreGameEvents.Instance.OnGamePause += OnGamePause;
            CoreGameEvents.Instance.OnGameResume += OnGameResume;
            CoreGameEvents.Instance.OnGameWin += OnGameWin;
            CoreGameEvents.Instance.OnGameLose += OnGameLose;
        }

        private void OnGamePause() => _currentState = GameState.Paused;
        private void OnGameResume() => _currentState = GameState.Playing;
        
        private void OnGameWin()
        {
            GameOver(true);
        }
        private void OnGameLose()
        {
            GameOver(false);
        }

        private void UnSubscribeEvents()
        {
            CoreGameEvents.Instance.OnGamePause -= OnGamePause;
            CoreGameEvents.Instance.OnGameResume -= OnGameResume;
            CoreGameEvents.Instance.OnGameWin -= OnGameWin;
            CoreGameEvents.Instance.OnGameLose -= OnGameLose;
        }

        private  void OnDisable()
        {
            UnSubscribeEvents();
        }
        
        private void GameOver(bool win)
        {
            _currentState = GameState.GameOver;
            CoreGameEvents.Instance.OnGamePause?.Invoke();
            UIEvents.Instance.onOpenPanel?.Invoke(win ? PanelTypes.Win : PanelTypes.Lose, 4);
        }

        public GameState GetCurrentState() => _currentState;

        // For Buttons
        public void InvokeGamePause() => CoreGameEvents.Instance.OnGamePause?.Invoke();
        public void InvokeGameResume() => CoreGameEvents.Instance.OnGameResume?.Invoke();
        
    }
}