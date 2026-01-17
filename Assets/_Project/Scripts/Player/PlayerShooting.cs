using UnityEngine;

namespace _Project.Scripts.Player
{
    /// <summary>
    /// This component handles the player's shooting mechanics based on input and configuration.
    /// </summary>
    public sealed class PlayerShooting : MonoBehaviour
    {
        [SerializeField] private PlayerConfig config;
        [SerializeField] private Transform firePoint;
        [SerializeField] private GameObject bulletPrefab;

        private float _cooldownRemaining;

        private void Awake()
        {
            if (firePoint == null) firePoint = transform;
        }

        private void Update()
        {
            if (_cooldownRemaining > 0f) _cooldownRemaining -= Time.deltaTime;
        }

        public void TickShooting(bool fireHeld, Vector2 aimDirectionRaw)
        {
            if (!fireHeld) return;
            if (_cooldownRemaining > 0f) return;

            if (config == null || bulletPrefab == null) return;

            var aimDirection = aimDirectionRaw.sqrMagnitude > 0.0001f ? aimDirectionRaw.normalized : Vector2.right;

            var bulletGo = ObjectPoolManager.Spawn(bulletPrefab, firePoint.position, Quaternion.identity);

            if (bulletGo == null)
                return;

            if (bulletGo.TryGetComponent(out Rigidbody2D rb))
            {
                rb.gravityScale = 0f;
                rb.freezeRotation = true;
                rb.linearVelocity = aimDirection * config.bulletSpeed;
            }

            _cooldownRemaining = config.fireCooldownSeconds;
        }
    }
}

