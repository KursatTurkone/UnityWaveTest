using System;
using _Project.Scripts.Core;
using _Project.Scripts.Player;
using UnityEngine;

namespace _Project.Scripts.Enemies
{
    public class Enemy : MonoBehaviour
    {
        [Header("Stats")]
        [SerializeField] private int hp = 3;
        [SerializeField] private float contactDamageCooldown = 0.6f;
        [SerializeField] private int pointsPerKill = 10;

        private float _lastContactDamageTime = -999f;
        private int _initialHp;

        private void Awake()
        {
            _initialHp = hp;
        }

        private void OnEnable()
        {
            // Reset HP or other stats if necessary
            _lastContactDamageTime = -999f;
            hp = _initialHp;
        }

        public void TakeDamage(int dmg)
        {
            hp -= dmg;

            if (hp <= 0)
            {
                Die();
            }
        }

        private void OnTriggerStay2D(Collider2D other)
        {
            if (other.TryGetComponent<Bullet>(out var bullet))
            {
                TakeDamage(1); 
                bullet.NotifyHit();
                return;
            }

            if (other.TryGetComponent<PlayerController>(out var playerController))
            {
                if (Time.time - _lastContactDamageTime < contactDamageCooldown) return;
                _lastContactDamageTime = Time.time;

                playerController.ApplyDamage(1);
            }
        }

        private void Die()
        {
            EventBus.Instance.Publish_EnemyDied(pointsPerKill);
            ObjectPoolManager.Despawn(gameObject);
        }
    }
}
