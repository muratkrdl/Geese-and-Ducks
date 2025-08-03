using UnityEngine;
using UnityEngine.Events;

namespace Murat.Events
{
    public class CoreGameEvents : MonoBehaviour
    {
        public static CoreGameEvents Instance;

        public UnityAction OnGamePause;
        public UnityAction OnGameResume;
        public UnityAction OnGameWin;
        public UnityAction OnGameLose;

        private void Awake()
        {
            Instance = this;
        }
    }
}