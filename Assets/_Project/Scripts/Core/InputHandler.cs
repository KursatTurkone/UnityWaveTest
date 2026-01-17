using Case.UnityWaveTest.EventBus;
using UnityEngine;

namespace _Project.Scripts.Core
{
    public class InputHandler : MonoBehaviour
    {
        [Header("Input Settings")]
        [SerializeField] private bool allowPause = true;
        [SerializeField] private KeyCode pauseKey = KeyCode.Escape;
        [SerializeField] private KeyCode restartKey = KeyCode.R;

        private bool _isPaused;

        private void Update()
        {
            HandlePauseInput();
            HandleRestartInput();
        }

        private void HandlePauseInput()
        {
            if (allowPause && Input.GetKeyDown(pauseKey))
            {
                _isPaused = !_isPaused;
                SimpleEventBus.Publish(new OnGamePausedEvent { IsPaused = _isPaused });
            }
        }

        private void HandleRestartInput()
        {
            if (Input.GetKeyDown(restartKey))
            {
                SimpleEventBus.Publish(new OnGameRestartEvent());
            }
        }
        
    }
}

