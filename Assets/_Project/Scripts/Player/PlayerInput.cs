using UnityEngine;

namespace _Project.Scripts.Player
{
    /// <summary>
    /// This component reads and processes player input based on the provided PlayerConfig.
    /// </summary>
    public sealed class PlayerInput : MonoBehaviour
    {
        [SerializeField] private PlayerConfig config;

        public bool IsEnabled { get; private set; } = true;

        public readonly struct Snapshot
        {
            public readonly Vector2 Move;
            public readonly bool    FireHeld;
            public readonly Vector2 MousePosition;

            public Snapshot(Vector2 move, bool fireHeld, Vector2 mousePosition)
            {
                Move          = move;
                FireHeld      = fireHeld;
                MousePosition = mousePosition;
            }
        }

        public void SetEnabled(bool enabled) => IsEnabled = enabled;

        public Snapshot Read()
        {
            if (!IsEnabled || config == null) return new Snapshot(Vector2.zero, false, Vector2.zero);

            var xInputRaw = Input.GetAxisRaw(config.horizontalAxis);
            var yInputRaw = Input.GetAxisRaw(config.verticalAxis);
            var mousePos  = Input.mousePosition;

            var move = new Vector2(xInputRaw, yInputRaw);
            if (config.normalizeMovementInput && move.sqrMagnitude > 1f) move.Normalize();

            var fireHeld = Input.GetButton(config.fireButton);
            return new Snapshot(move, fireHeld, mousePos);
        }
    }
}