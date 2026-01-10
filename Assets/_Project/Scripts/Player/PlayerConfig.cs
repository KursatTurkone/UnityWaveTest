using UnityEngine;

namespace _Project.Scripts.Player
{
    /// <summary>
    /// This ScriptableObject holds configuration data for the player character.
    /// </summary>
    [CreateAssetMenu(menuName = "Project/Player Config", fileName = "PlayerConfig")]
    public sealed class PlayerConfig : ScriptableObject
    {
        [Header("Movement"), Min(0f)]    public float moveSpeed           = 6f;
        [Header("Shooting"), Min(0.05f)] public float fireCooldownSeconds = 0.15f;
        [Min(                    0.1f)]  public float bulletSpeed         = 14f;
        [Min(                    0.1f)]  public float bulletDuration  = 5f;
        [Header("Health"), Min(  1)]     public int   maxHealth           = 5;

        [Header("Input")] public string horizontalAxis = "Horizontal";

        public string verticalAxis = "Vertical";
        public string fireButton   = "Fire1";

        [Header("Optional")] public bool normalizeMovementInput = true;
    }
}