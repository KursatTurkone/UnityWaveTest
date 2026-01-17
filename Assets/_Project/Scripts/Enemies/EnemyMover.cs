using Case.UnityWaveTest.EventBus;
using UnityEngine;

namespace _Project.Scripts.Enemies
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class EnemyMover : MonoBehaviour
    {
        [SerializeField] private float speed = 2.25f;
        [SerializeField] private float jitter = 0.15f;

        private Rigidbody2D _rigidbody;
        private Transform _playerTransform;

        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody2D>();
            _rigidbody.gravityScale = 0f;
            _rigidbody.freezeRotation = true;
        }

        private void OnEnable()
        {
            SimpleEventBus.Subscribe<OnPlayerSpawnedEvent>(HandlePlayerSpawned);
        }

        private void OnDisable()
        {
            SimpleEventBus.Unsubscribe<OnPlayerSpawnedEvent>(HandlePlayerSpawned);
        }

        private void HandlePlayerSpawned(OnPlayerSpawnedEvent evt)
        {
            _playerTransform = evt.PlayerTransform;
        }

        public void SetPlayerTransform(Transform playerTransform)
        {
            _playerTransform = playerTransform;
        }

        private void FixedUpdate()
        {
            if (_playerTransform == null)
            {
                _rigidbody.linearVelocity = new Vector2(Mathf.Sin(Time.time) * 0.5f, Mathf.Cos(Time.time) * 0.5f);
                return;
            }

            Vector2 dir = ((Vector2)_playerTransform.position - _rigidbody.position);
            if (dir.sqrMagnitude < 0.0001f) return;

            dir.Normalize();
            dir += Random.insideUnitCircle * jitter;
            dir.Normalize();

            _rigidbody.linearVelocity = dir * speed;
        }
    }
}
