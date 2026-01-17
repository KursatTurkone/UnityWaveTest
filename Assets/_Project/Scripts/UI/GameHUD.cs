using Case.UnityWaveTest.EventBus;
using UnityEngine;
using UnityEngine.UI;

namespace _Project.Scripts.UI
{
    public class GameHUD : MonoBehaviour
    {
        [Header("UI Elements")]
        [SerializeField] private Text scoreText;
        [SerializeField] private Text waveText;
        [SerializeField] private GameObject gameOverPanel;

        private void OnEnable()
        {
            SimpleEventBus.Subscribe<OnScoreChangedEvent>(HandleScoreChanged);
            SimpleEventBus.Subscribe<OnWaveStartedEvent>(HandleWaveStarted);
            SimpleEventBus.Subscribe<OnGameOverEvent>(HandleGameOver);
            SimpleEventBus.Subscribe<OnGameRestartEvent>(HandleGameRestart);
        }

        private void OnDisable()
        {
            SimpleEventBus.Unsubscribe<OnScoreChangedEvent>(HandleScoreChanged);
            SimpleEventBus.Unsubscribe<OnWaveStartedEvent>(HandleWaveStarted);
            SimpleEventBus.Unsubscribe<OnGameOverEvent>(HandleGameOver);
            SimpleEventBus.Unsubscribe<OnGameRestartEvent>(HandleGameRestart);
        }

        private void Start()
        {
            if (gameOverPanel != null)
                gameOverPanel.SetActive(false);
        }

        private void HandleScoreChanged(OnScoreChangedEvent evt)
        {
            if (scoreText != null)
                scoreText.text = $"Score: {evt.NewScore}";
        }

        private void HandleWaveStarted(OnWaveStartedEvent evt)
        {
            if (waveText != null)
                waveText.text = $"Wave: {evt.WaveNumber}";
        }

        private void HandleGameOver(OnGameOverEvent evt)
        {
            if (gameOverPanel != null)
                gameOverPanel.SetActive(true);
        }

        private void HandleGameRestart(OnGameRestartEvent evt)
        {
            if (gameOverPanel != null)
                gameOverPanel.SetActive(false);
        }
    }
}

