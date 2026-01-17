using _Project.Scripts.Enemies;
using _Project.Scripts.Player;
using Case.UnityWaveTest.EventBus;
using UnityEngine;

namespace _Project.Scripts.Core
{
    public class GameStateManager : MonoBehaviour
    {
        [Header("Score Settings")] [SerializeField]
        private int pointsPerKill = 10;

        [Header("Player")] [SerializeField] private GameObject playerPrefab;
        [SerializeField] private Transform playerSpawnPoint;
        private GameObject currentPlayer;

        private int _score;
        private int _currentWave = 1;
        private bool _isGameOver;
        private bool _isPaused;

        public bool IsGameOver => _isGameOver;
        public bool IsPaused => _isPaused;
        public int PointsPerKill => pointsPerKill;
        public Transform CurrentPlayerTransform => currentPlayer != null ? currentPlayer.transform : null;

        private void OnEnable()
        {
            SimpleEventBus.Subscribe<OnEnemyDiedEvent>(HandleEnemyDied);
            SimpleEventBus.Subscribe<OnPlayerDiedEvent>(HandlePlayerDied);
            SimpleEventBus.Subscribe<OnGameRestartEvent>(HandleGameRestart);
            SimpleEventBus.Subscribe<OnGamePausedEvent>(HandleGamePaused);
            SimpleEventBus.Subscribe<OnWaveStartedEvent>(HandleWaveStarted);
        }

        private void OnDisable()
        {
            SimpleEventBus.Unsubscribe<OnEnemyDiedEvent>(HandleEnemyDied);
            SimpleEventBus.Unsubscribe<OnPlayerDiedEvent>(HandlePlayerDied);
            SimpleEventBus.Unsubscribe<OnGameRestartEvent>(HandleGameRestart);
            SimpleEventBus.Unsubscribe<OnGamePausedEvent>(HandleGamePaused);
            SimpleEventBus.Unsubscribe<OnWaveStartedEvent>(HandleWaveStarted);
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
            SimpleEventBus.Publish(new OnScoreChangedEvent { NewScore = _score });
        }

        private void EnsurePlayerExists()
        {
            if (playerPrefab == null) return;

            Vector3 spawnPos = playerSpawnPoint != null ? playerSpawnPoint.position : Vector3.zero;
            if (currentPlayer == null)
            {
                currentPlayer = Instantiate(playerPrefab, spawnPos, Quaternion.identity);
                SimpleEventBus.Publish(new OnPlayerSpawnedEvent { PlayerTransform = currentPlayer.transform });
            }
        }

        private void HandleEnemyDied(OnEnemyDiedEvent evt)
        {
            if (_isGameOver) return;

            _score += evt.ScoreGained;
            SimpleEventBus.Publish(new OnScoreChangedEvent { NewScore = _score });
        }

        private void HandlePlayerDied(OnPlayerDiedEvent evt)
        {
            if (_isGameOver) return;

            _isGameOver = true;
            SimpleEventBus.Publish(new OnGameOverEvent { FinalScore = _score, WaveReached = _currentWave });
        }

        private void HandleGameRestart(OnGameRestartEvent evt)
        {
            DestroyAllEnemies();
            DestroyAllBullets();
            InitializeGame();
            ResetPlayer();
        }

        private void ResetPlayer()
        {
            if (currentPlayer == null) return;

            if (playerSpawnPoint != null)
            {
                currentPlayer.transform.position = playerSpawnPoint.position;
                currentPlayer.transform.rotation = Quaternion.identity;
            }

            if (currentPlayer.TryGetComponent<PlayerHealth>(out var playerHealth))
            {
                playerHealth.ResetToFull();
            }

            if (currentPlayer.TryGetComponent<Rigidbody2D>(out var rb))
            {
                rb.linearVelocity = Vector2.zero;
                rb.angularVelocity = 0f;
            }

            currentPlayer.SetActive(true);
        }

        private void HandleGamePaused(OnGamePausedEvent evt)
        {
            _isPaused = evt.IsPaused;
            Time.timeScale = evt.IsPaused ? 0f : 1f;
        }

        private void HandleWaveStarted(OnWaveStartedEvent evt)
        {
            _currentWave = evt.WaveNumber;
        }

        private void DestroyAllEnemies()
        {
            var enemies = FindObjectsByType<Enemy>(FindObjectsSortMode.None);
            foreach (var enemy in enemies)
            {
                if (enemy.gameObject.activeSelf)
                {
                    enemy.ForceDespawn();
                }
            }
        }

        private void DestroyAllBullets()
        {
            var bullets = FindObjectsByType<Bullet>(FindObjectsSortMode.None);
            foreach (var bullet in bullets)
            {
                if (bullet.gameObject.activeSelf)
                {
                    bullet.Despawn();
                }
            }
        }
    }
}