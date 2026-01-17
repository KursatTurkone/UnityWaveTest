using UnityEngine;

namespace _Project.Scripts.Core
{
    public class GameStateManager : MonoBehaviour
    {
        [Header("Score Settings")]
        [SerializeField] private int pointsPerKill = 10;

        [Header("Player")]
        [SerializeField] private GameObject playerPrefab;
        [SerializeField] private Transform playerSpawnPoint;
        [SerializeField] private GameObject currentPlayer;

        private int _score;
        private int _currentWave = 1;
        private bool _isGameOver;
        private bool _isPaused;

        public bool IsGameOver => _isGameOver;
        public bool IsPaused => _isPaused;
        public int PointsPerKill => pointsPerKill;

        private void OnEnable()
        {
            EventBus.Instance.OnEnemyDied += HandleEnemyDied;
            EventBus.Instance.OnPlayerDied += HandlePlayerDied;
            EventBus.Instance.OnGameRestart += HandleGameRestart;
            EventBus.Instance.OnGamePaused += HandleGamePaused;
            EventBus.Instance.OnWaveStarted += HandleWaveStarted;
        }

        private void OnDisable()
        {
            if (EventBus.Instance == null) return;

            EventBus.Instance.OnEnemyDied -= HandleEnemyDied;
            EventBus.Instance.OnPlayerDied -= HandlePlayerDied;
            EventBus.Instance.OnGameRestart -= HandleGameRestart;
            EventBus.Instance.OnGamePaused -= HandleGamePaused;
            EventBus.Instance.OnWaveStarted -= HandleWaveStarted;
        }

        private void Start()
        {
            InitializeGame();
        }

        private void InitializeGame()
        {
            _score = 0;
            _currentWave = 1;
            _isGameOver = false;
            _isPaused = false;
            Time.timeScale = 1f;

            EnsurePlayerExists();
            EventBus.Instance.Publish_ScoreChanged(_score);
        }

        private void EnsurePlayerExists()
        {
            if (currentPlayer != null) return;

            if (playerPrefab == null) return;

            Vector3 spawnPos = playerSpawnPoint != null ? playerSpawnPoint.position : Vector3.zero;
            currentPlayer = Instantiate(playerPrefab, spawnPos, Quaternion.identity);
            EventBus.Instance.Publish_PlayerSpawned(currentPlayer.transform);
        }

        private void HandleEnemyDied(int pointsGained)
        {
            if (_isGameOver) return;

            _score += pointsGained;
            EventBus.Instance.Publish_ScoreChanged(_score);
        }

        private void HandlePlayerDied()
        {
            if (_isGameOver) return;

            _isGameOver = true;
            currentPlayer = null;
            EventBus.Instance.Publish_GameOver(_score, _currentWave);
        }

        private void HandleGameRestart()
        {
            DestroyAllEnemies();
            InitializeGame();
        }

        private void HandleGamePaused(bool paused)
        {
            _isPaused = paused;
            Time.timeScale = paused ? 0f : 1f;
        }

        private void HandleWaveStarted(int waveNumber)
        {
            _currentWave = waveNumber;
        }

        private void DestroyAllEnemies()
        {
            var enemies = FindObjectsByType<Enemy>(FindObjectsSortMode.None);
            foreach (var enemy in enemies)
            {
                Destroy(enemy.gameObject);
            }
        }
    }
}

