using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Header("Legacy Stats")]
    public int hp = 3;
    public float contactDamageCooldown = 0.6f;

    [Header("References (legacy)")]
    public GameStuff game;
    public MonoBehaviour uiStuff;

    float _lastContactDamageTime = -999f;

    void Awake()
    {
        // Find refs lazily (coupled, slow)
        if (game == null) game = FindObjectOfType<GameStuff>();
        if (uiStuff == null) uiStuff = FindObjectOfType<UIStuff>();
    }

    public void TakeDamage(int dmg)
    {
        hp -= dmg;

        if (hp <= 0)
        {
            DieLegacy();
        }
    }

    void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag(GlobalVars.BulletTag))
        {
            TakeDamage(1);

            Destroy(other.gameObject);
            return;
        }

        if (other.CompareTag(GlobalVars.PlayerTag))
        {
            if (Time.time - _lastContactDamageTime < contactDamageCooldown) return;
            _lastContactDamageTime = Time.time;

            other.SendMessage("ApplyDamage", 1, SendMessageOptions.DontRequireReceiver);
        }
    }

    void DieLegacy()
    {
        if (game != null)
        {
            game.NotifyEnemyDied();
        }
        else
        {
            GlobalVars.Score += GlobalVars.PointsPerKill;
        }

        if (uiStuff != null)
        {
            uiStuff.SendMessage("PulseScore", SendMessageOptions.DontRequireReceiver);
        }

        Destroy(gameObject);
    }
}
