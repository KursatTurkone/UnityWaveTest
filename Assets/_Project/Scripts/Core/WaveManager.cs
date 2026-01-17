using System.Collections;
using _Project.Scripts.Enemies;
using Case.UnityWaveTest.EventBus;
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
            SimpleEventBus.Subscribe<OnEnemyDiedEvent>(HandleEnemyDied);
            SimpleEventBus.Subscribe<OnGameRestartEvent>(HandleGameRestart);
        }

        private void OnDisable()
        {
            SimpleEventBus.Unsubscribe<OnEnemyDiedEvent>(HandleEnemyDied);
            SimpleEventBus.Unsubscribe<OnGameRestartEvent>(HandleGameRestart);
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

            SimpleEventBus.Publish(new OnWaveStartedEvent { WaveNumber = _currentWave });

            if (_spawnCoroutine != null)
                StopCoroutine(_spawnCoroutine);

            _spawnCoroutine = StartCoroutine(SpawnWaveEnemies());
        }

        private IEnumerator SpawnWaveEnemies()
        {
            yield return new WaitForEndOfFrame();
            int enemyCount = GetEnemyCountForWave(_currentWave);

            for (int i = 0; i < enemyCount; i++)
            {
                Debug.Log("Spawning enemy " + (i + 1) + " of wave " + _currentWave);
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

        private void HandleEnemyDied(OnEnemyDiedEvent evt)
        {
            _aliveEnemies--;

            if (_aliveEnemies < 0)
            {
                _aliveEnemies = 0;
                return;
            }

            if (_aliveEnemies <= 0 && !_isSpawning)
            {
                SimpleEventBus.Publish(new OnWaveCompletedEvent { WaveNumber = _currentWave });
                StartCoroutine(StartNextWaveAfterDelay());
            }
        }

        private IEnumerator StartNextWaveAfterDelay()
        {
            yield return new WaitForSeconds(breakBetweenWaves);
            StartWave(_currentWave + 1);
        }

        private void HandleGameRestart(OnGameRestartEvent evt)
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

