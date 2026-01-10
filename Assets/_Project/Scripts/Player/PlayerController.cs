using UnityEngine;

namespace _Project.Scripts.Player
{
    /// <summary>
    /// This component coordinates player input, movement, shooting, and health from sub-components.
    /// </summary>
    [DisallowMultipleComponent]
    public sealed class PlayerController : MonoBehaviour
    {
        [SerializeField] private PlayerInput    input;
        [SerializeField] private PlayerMovement movement;
        [SerializeField] private PlayerShooting shooting;
        [SerializeField] private PlayerHealth   health;

        private void Awake()
        {
            if (input    == null) input    = GetComponent<PlayerInput>();
            if (movement == null) movement = GetComponent<PlayerMovement>();
            if (shooting == null) shooting = GetComponent<PlayerShooting>();
            if (health   == null) health   = GetComponent<PlayerHealth>();
        }

        private void Update()
        {
            // Respect legacy global end state (keeps player “solid” but compatible with messy project).
            if (GlobalVars.GameIsOver || GlobalVars.GameIsPaused)
            {
                input?.SetEnabled(false);
                movement?.Stop();
                return;
            }

            input?.SetEnabled(true);

            var snap = input != null ? input.Read() : default;

            var mouseDir = Vector2.zero;

            if (input != null)
            {
                Vector3 mouseWorldPos = Camera.main != null
                    ? Camera.main.ScreenToWorldPoint(snap.MousePosition)
                    : snap.MousePosition;
                mouseDir = mouseWorldPos - transform.position;
            }

            movement?.TickMovement(snap.Move);
            shooting?.TickShooting(snap.FireHeld, mouseDir);
        }

        // Legacy compatibility: Enemy.cs calls SendMessage("ApplyDamage", 1)
        // We keep this tiny bridge here so other Player scripts remain pure.
        public void ApplyDamage(int amount) { health?.ApplyDamage(amount); }
    }
}