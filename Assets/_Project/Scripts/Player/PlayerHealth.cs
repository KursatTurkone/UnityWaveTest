using System;
using UnityEngine;

namespace _Project.Scripts.Player
{
    /// <summary>
    /// This component manages the player's health, including applying damage and notifying listeners of health changes and death.
    /// </summary>
    public sealed class PlayerHealth : MonoBehaviour
    {
        [SerializeField] private PlayerConfig config;

        public int Current { get; private set; }
        public int Max     => config != null ? config.maxHealth : 1;

        public event Action<int, int> HealthChanged; // (current, max)
        public event Action           Died;

        private bool _isDead;

        private void Awake()
        {
            ResetToFull();
        }

        public void ResetToFull()
        {
            _isDead = false;
            Current = Mathf.Clamp(Max, 1, int.MaxValue);
            HealthChanged?.Invoke(Current, Max);
        }

        public void ApplyDamage(int amount)
        {
            if (_isDead) return;
            if (amount <= 0) return;

            Current = Mathf.Max(0, Current - amount);
            HealthChanged?.Invoke(Current, Max);

            if (Current <= 0)
            {
                _isDead = true;
                Died?.Invoke();
            }
        }
    }
}