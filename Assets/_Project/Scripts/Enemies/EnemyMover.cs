using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class EnemyMover : MonoBehaviour
{
    public float speed  = 2.25f;
    public float jitter = 0.15f;

    Rigidbody2D _rb;
    Transform   ply;
    float       _timer;

    void Awake()
    {
        _rb                = GetComponent<Rigidbody2D>();
        _rb.gravityScale   = 0f;
        _rb.freezeRotation = true;

        FindPlayer();
    }

    void Update()
    {
        if (GlobalVars.GameIsOver) return;

        _timer += Time.deltaTime;
        if (ply == null || _timer > 0.7f)
        {
            _timer = 0f;
            FindPlayer();
        }
    }

    void FixedUpdate()
    {
        if (GlobalVars.GameIsOver) return;

        if (ply == null)
        {
            _rb.linearVelocity = new Vector2(Mathf.Sin(Time.time) * 0.5f, Mathf.Cos(Time.time) * 0.5f);
            return;
        }

        Vector2 dir = ((Vector2)ply.position - _rb.position);
        if (dir.sqrMagnitude < 0.0001f) return;

        dir.Normalize();

        dir += Random.insideUnitCircle * jitter;
        dir.Normalize();

        _rb.linearVelocity = dir * speed;
    }

    void FindPlayer()
    {
        var p = GameObject.FindGameObjectWithTag(GlobalVars.PlayerTag);
        ply = p != null ? p.transform : null;
    }
}