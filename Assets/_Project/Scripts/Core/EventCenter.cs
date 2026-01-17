using System;
using UnityEngine;

namespace _Project.Scripts.Core
{
    public class EventCenter : MonoBehaviour
    {
        private static EventCenter _instance;
        
        public static EventCenter Instance
        {
            get
            {
                if (_instance == null)
                {
                    var go = new GameObject("[EventBus]");
                    _instance = go.AddComponent<EventCenter>();
                    DontDestroyOnLoad(go);
                }
                return _instance;
            }
        }

        public event Action<int> OnEnemyDied;
        public event Action<int> OnWaveStarted;
        public event Action<int> OnWaveCompleted;
        public event Action<int> OnScoreChanged;
        public event Action<int, int> OnGameOver;
        public event Action OnGameRestart;
        public event Action<Transform> OnPlayerSpawned;
        public event Action OnPlayerDied;
        public event Action<bool> OnGamePaused;

        private void Awake()
        {
            if (_instance != null && _instance != this)
            {
                Destroy(gameObject);
                return;
            }
            
            _instance = this;
            DontDestroyOnLoad(gameObject);
        }

        public void Publish_EnemyDied(int scoreGained)
        {
            OnEnemyDied?.Invoke(scoreGained);
        }

        public void Publish_WaveStarted(int waveNumber)
        {
            OnWaveStarted?.Invoke(waveNumber);
        }

        public void Publish_WaveCompleted(int waveNumber)
        {
            OnWaveCompleted?.Invoke(waveNumber);
        }

        public void Publish_ScoreChanged(int newScore)
        {
            OnScoreChanged?.Invoke(newScore);
        }

        public void Publish_GameOver(int finalScore, int finalWave)
        {
            OnGameOver?.Invoke(finalScore, finalWave);
        }

        public void Publish_GameRestart()
        {
            OnGameRestart?.Invoke();
        }

        public void Publish_PlayerSpawned(Transform playerTransform)
        {
            OnPlayerSpawned?.Invoke(playerTransform);
        }

        public void Publish_PlayerDied()
        {
            OnPlayerDied?.Invoke();
        }

        public void Publish_GamePaused(bool isPaused)
        {
            OnGamePaused?.Invoke(isPaused);
        }

        public void ClearAllSubscriptions()
        {
            OnEnemyDied = null;
            OnWaveStarted = null;
            OnWaveCompleted = null;
            OnScoreChanged = null;
            OnGameOver = null;
            OnGameRestart = null;
            OnPlayerSpawned = null;
            OnPlayerDied = null;
            OnGamePaused = null;
        }

        private void OnDestroy()
        {
            if (_instance == this)
            {
                ClearAllSubscriptions();
            }
        }
    }
}

