using Case.UnityWaveTest.EventBus;
using UnityEngine;

namespace _Project.Scripts.Enemies
{
    public class EnemySpawner : MonoBehaviour
    {
        [Header("Prefab")]
        [SerializeField] private GameObject enemyPrefab;

        [Header("Spawn Points")]
        [SerializeField] private Transform[] spawnPoints;

        [Header("Fallback Settings")]
        [SerializeField] private bool randomizeIfNoPoints = true;
        [SerializeField] private float spawnRadiusIfNoPoints = 7f;

        private int _currentIndex;
        private Transform _playerTransform;

        private void Awake()
        {
            if (spawnPoints == null || spawnPoints.Length == 0)
            {
                GenerateDefaultSpawnPoints();
            }
        }

        private void OnEnable()
        {
            SimpleEventBus.Subscribe<OnPlayerSpawnedEvent>(HandlePlayerSpawned);
        }

        private void OnDisable()
        {
            SimpleEventBus.Unsubscribe<OnPlayerSpawnedEvent>(HandlePlayerSpawned);
        }

        private void HandlePlayerSpawned(OnPlayerSpawnedEvent evt)
        {
            _playerTransform = evt.PlayerTransform;
        }

        public void SpawnOneEnemy()
        {
            if (enemyPrefab == null) return;

            Vector3 spawnPosition = GetSpawnPosition();
            var enemyObject = ObjectPoolManager.Spawn(enemyPrefab, spawnPosition, Quaternion.identity);

            if (enemyObject == null)
                return;


            if (_playerTransform != null && enemyObject.TryGetComponent<EnemyMover>(out var enemyMover))
            {
                enemyMover.SetPlayerTransform(_playerTransform);
            }
        }

        private Vector3 GetSpawnPosition()
        {
            if (spawnPoints != null && spawnPoints.Length > 0)
            {
                _currentIndex++;
                if (_currentIndex >= spawnPoints.Length)
                    _currentIndex = 0;

                if (spawnPoints[_currentIndex] != null)
                    return spawnPoints[_currentIndex].position;
            }

            if (randomizeIfNoPoints)
            {
                var randomDirection = Random.insideUnitCircle.normalized * spawnRadiusIfNoPoints;
                return new Vector3(randomDirection.x, randomDirection.y, 0f);
            }

            return Vector3.zero;
        }

        private void GenerateDefaultSpawnPoints()
        {
            spawnPoints = new Transform[4];
            for (int i = 0; i < spawnPoints.Length; i++)
            {
                var spawnPointObject = new GameObject($"SpawnPoint_{i}");
                spawnPointObject.transform.position = i switch
                {
                    0 => new Vector3(8, 0, 0),
                    1 => new Vector3(-8, 0, 0),
                    2 => new Vector3(0, 8, 0),
                    _ => new Vector3(0, -8, 0),
                };
                spawnPoints[i] = spawnPointObject.transform;
            }
        }
    }
}