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
            CoreGameEvents.Instance.OnGamePause += OnGamePause;
            CoreGameEvents.Instance.OnGameResume += OnGameResume;
        }

        private void OnGamePause() => _currentState = GameState.Paused;
        private void OnGameResume() => _currentState = GameState.Playing;

        private  void OnDisable()
        {
            CoreGameEvents.Instance.OnGamePause -= OnGamePause;
            CoreGameEvents.Instance.OnGameResume -= OnGameResume;
        }

        public GameState GetCurrentState() => _currentState;

        public void InvokeGamePause() => CoreGameEvents.Instance.OnGamePause?.Invoke();
        public void InvokeGameResume() => CoreGameEvents.Instance.OnGameResume?.Invoke();
        
    }
}