using _Project.Scripts.Core;
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
            EventBus.Instance.OnScoreChanged += HandleScoreChanged;
            EventBus.Instance.OnWaveStarted += HandleWaveStarted;
            EventBus.Instance.OnGameOver += HandleGameOver;
            EventBus.Instance.OnGameRestart += HandleGameRestart;
        }

        private void OnDisable()
        {
            if (EventBus.Instance == null) return;

            EventBus.Instance.OnScoreChanged -= HandleScoreChanged;
            EventBus.Instance.OnWaveStarted -= HandleWaveStarted;
            EventBus.Instance.OnGameOver -= HandleGameOver;
            EventBus.Instance.OnGameRestart -= HandleGameRestart;
        }

        private void Start()
        {
            if (gameOverPanel != null)
                gameOverPanel.SetActive(false);
        }

        private void HandleScoreChanged(int newScore)
        {
            if (scoreText != null)
                scoreText.text = $"Score: {newScore}";
        }

        private void HandleWaveStarted(int waveNumber)
        {
            if (waveText != null)
                waveText.text = $"Wave: {waveNumber}";
        }

        private void HandleGameOver(int finalScore, int finalWave)
        {
            if (gameOverPanel != null)
                gameOverPanel.SetActive(true);
        }

        private void HandleGameRestart()
        {
            if (gameOverPanel != null)
                gameOverPanel.SetActive(false);
        }
    }
}

