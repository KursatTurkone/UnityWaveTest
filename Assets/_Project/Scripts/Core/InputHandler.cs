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
            HandleDebugInput();
        }

        private void HandlePauseInput()
        {
            if (allowPause && Input.GetKeyDown(pauseKey))
            {
                _isPaused = !_isPaused;
                EventBus.Instance.Publish_GamePaused(_isPaused);
            }
        }

        private void HandleRestartInput()
        {
            if (Input.GetKeyDown(restartKey))
            {
                EventBus.Instance.Publish_GameRestart();
            }
        }

        private void HandleDebugInput()
        {
            if (Input.GetKeyDown(KeyCode.F1))
            {
                GlobalVars.VerboseLogs = !GlobalVars.VerboseLogs;
                Debug.Log($"VerboseLogs = {GlobalVars.VerboseLogs}");
            }
        }
    }
}