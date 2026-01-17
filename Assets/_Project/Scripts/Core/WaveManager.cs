using System.Collections;
using UnityEngine;

namespace _Project.Scripts.Core
{
    public class WaveManager : MonoBehaviour
    {
        [Header("Wave Settings")]
        [SerializeField] private int baseEnemiesPerWave = 5;
        [SerializeField] private int enemiesAddedPerWave = 3;
        [SerializeField] private float spawnInterval = 0.5f;
        [SerializeField] private float breakBetweenWaves = 3.0f;

        [Header("References")]
        [SerializeField] private EnemySpawner spawner;

        private int _currentWave = 1;
        private int _aliveEnemies;
        private bool _isSpawning;
        private Coroutine _spawnCoroutine;

        private void OnEnable()
        {
            EventBus.Instance.OnEnemyDied += HandleEnemyDied;
            EventBus.Instance.OnGameRestart += HandleGameRestart;
        }

        private void OnDisable()
        {
            if (EventBus.Instance == null) return;

            EventBus.Instance.OnEnemyDied -= HandleEnemyDied;
            EventBus.Instance.OnGameRestart -= HandleGameRestart;
        }

        private void Start()
        {
            StartWave(_currentWave);
        }

        private void StartWave(int waveNumber)
        {
            _currentWave = waveNumber;
            _aliveEnemies = 0;
            _isSpawning = true;

            EventBus.Instance.Publish_WaveStarted(_currentWave);

            if (_spawnCoroutine != null)
                StopCoroutine(_spawnCoroutine);

            _spawnCoroutine = StartCoroutine(SpawnWaveEnemies());
        }

        private IEnumerator SpawnWaveEnemies()
        {
            int enemyCount = GetEnemyCountForWave(_currentWave);

            for (int i = 0; i < enemyCount; i++)
            {
                spawner.SpawnOneEnemy();
                _aliveEnemies++;
                yield return new WaitForSeconds(spawnInterval);
            }

            _isSpawning = false;
        }

        private int GetEnemyCountForWave(int waveNumber)
        {
            return baseEnemiesPerWave + (waveNumber - 1) * enemiesAddedPerWave;
        }

        private void HandleEnemyDied(int points)
        {
            _aliveEnemies--;

            if (_aliveEnemies <= 0 && !_isSpawning)
            {
                EventBus.Instance.Publish_WaveCompleted(_currentWave);
                StartCoroutine(StartNextWaveAfterDelay());
            }
        }

        private IEnumerator StartNextWaveAfterDelay()
        {
            yield return new WaitForSeconds(breakBetweenWaves);
            StartWave(_currentWave + 1);
        }

        private void HandleGameRestart()
        {
            if (_spawnCoroutine != null)
                StopCoroutine(_spawnCoroutine);

            StopAllCoroutines();

            _currentWave = 1;
            _aliveEnemies = 0;
            _isSpawning = false;

            StartWave(_currentWave);
        }
    }
}

