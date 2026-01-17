using _Project.Scripts.Core;
using UnityEngine;

namespace _Project.Scripts.Player
{
    /// <summary>
    /// This component listens for the player's death event and notifies the game state via EventBus.
    /// </summary>
    public sealed class PlayerDeathHandler : MonoBehaviour
    {
        [SerializeField] private PlayerHealth health;

        private void Awake()
        {
            if (health == null)
                health = GetComponent<PlayerHealth>();

            if (health != null)
                health.Died += OnDied;
        }

        private void OnDestroy()
        {
            if (health != null)
                health.Died -= OnDied;
        }

        private void OnDied()
        {
            EventBus.Instance.Publish_PlayerDied();
        }
    }
}