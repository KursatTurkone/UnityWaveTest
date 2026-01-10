using UnityEngine;

namespace _Project.Scripts.Player
{
    /// <summary>
    /// This component listens for the player's death event and notifies the game state controller.
    /// </summary>
    public sealed class PlayerDeathHandler : MonoBehaviour
    {
        [SerializeField] private PlayerHealth health;

        private GameStuff _gameStuff;

        private void Awake()
        {
            if (health == null)
                health = GetComponent<PlayerHealth>();

            _gameStuff = FindFirstObjectByType<GameStuff>();

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
            // One single bridge to legacy. Candidates can later route this into their game-state controller.
            if (_gameStuff != null)
                _gameStuff.NotifyPlayerDied();
            else
                GlobalVars.GameIsOver = true;
        }
    }
}