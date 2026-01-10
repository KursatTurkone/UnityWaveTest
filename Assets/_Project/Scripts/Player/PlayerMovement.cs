using UnityEngine;

namespace _Project.Scripts.Player
{
    /// <summary>
    /// This component handles the player's movement based on input and configuration.
    /// </summary>
    [RequireComponent(typeof(Rigidbody2D))]
    public sealed class PlayerMovement : MonoBehaviour
    {
        [SerializeField] private PlayerConfig config;

        private Rigidbody2D _rigidbody;

        private void Awake()
        {
            _rigidbody                = GetComponent<Rigidbody2D>();
            _rigidbody.gravityScale   = 0f;
            _rigidbody.freezeRotation = true;
        }

        public void TickMovement(Vector2 moveInput)
        {
            if (config == null)
                return;

            _rigidbody.linearVelocity = moveInput * config.moveSpeed;
        }

        public void Stop()
        {
            if (_rigidbody != null)
                _rigidbody.linearVelocity = Vector2.zero;
        }
    }
}