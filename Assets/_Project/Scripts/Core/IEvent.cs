using System;
using UnityEngine;

namespace Case.UnityWaveTest.EventBus
{
    public struct OnEnemyDiedEvent : IEvent
    {
        public int ScoreGained;
    }

    public struct OnWaveStartedEvent : IEvent
    {
        public int WaveNumber;
    }

    public struct OnWaveCompletedEvent : IEvent
    {
        public int WaveNumber;
    }

    public struct OnScoreChangedEvent : IEvent
    {
        public int NewScore;
    }

    public struct OnGameOverEvent : IEvent
    {
        public int FinalScore;
        public int WaveReached;
    }

    public struct OnGameRestartEvent : IEvent
    {
    }

    public class OnPlayerSpawnedEvent : IEvent
    {
        public Transform PlayerTransform;
    }

    public struct OnPlayerDiedEvent : IEvent
    {
    }

    public struct OnGamePausedEvent : IEvent
    {
        public bool IsPaused;
    }

    public interface IEvent
    {
    }
}
