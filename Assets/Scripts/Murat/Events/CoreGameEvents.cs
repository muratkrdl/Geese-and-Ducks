using UnityEngine;
using UnityEngine.Events;

namespace Murat.Events
{
    public class CoreGameEvents : MonoBehaviour
    {
        public static CoreGameEvents Instance;

        public UnityAction OnGamePause;
        public UnityAction OnGameResume;

        private void Awake()
        {
            Instance = this;
        }
    }
}