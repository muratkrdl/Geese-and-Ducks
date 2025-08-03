using Murat.Events;
using Murat.Managers;
using UnityEngine;

namespace Murat.Abstracts
{
    public abstract class GamePlayBehaviour : MonoBehaviour
    {
        protected virtual void OnEnable()
        {
            CoreGameEvents.Instance.OnGamePause += OnGamePause;
            CoreGameEvents.Instance.OnGameResume += OnGameResume;
        }

        protected abstract void OnGamePause();
        protected abstract void OnGameResume();

        protected virtual void OnDisable()
        {
            CoreGameEvents.Instance.OnGamePause -= OnGamePause;
            CoreGameEvents.Instance.OnGameResume -= OnGameResume;
        }
    }
}