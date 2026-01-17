using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float lifetime = 5f;

    private float _spawnTime;

    private void OnEnable()
    {
        _spawnTime = Time.time;
    }

    private void OnDisable()
    {
        if (TryGetComponent<Rigidbody2D>(out var rb))
        {
            rb.linearVelocity = Vector2.zero;
        }
    }

    private void Update()
    {
        if (Time.time - _spawnTime >= lifetime)
        {
           Despawn();
        }
    }

    public void NotifyHit()
    {
      Despawn();
    }

    public void Despawn()
    {
        ObjectPoolManager.Despawn(gameObject);
    }
}
