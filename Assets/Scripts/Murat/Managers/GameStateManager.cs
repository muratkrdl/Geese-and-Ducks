using Murat.Enums;
using UnityEngine;
using UnityEngine.Events;

namespace Murat.Managers
{
    public class GameStateManager : MonoBehaviour
    {
        public static GameStateManager Instance;
        
        private GameState _currentState;
        
        public UnityAction onGamePause;
        public UnityAction onGameResume;

        private void Awake()
        {
            Instance = this;
            _currentState = GameState.Playing;
        }

        private void OnEnable()
        {
            onGamePause += OnGamePause;
            onGameResume += OnGameResume;
        }

        private void OnGamePause() => _currentState = GameState.Paused;
        private void OnGameResume() => _currentState = GameState.Playing;

        private  void OnDisable()
        {
            onGamePause -= OnGamePause;
            onGameResume -= OnGameResume;
        }

        public GameState GetCurrentState() => _currentState;

    }
}