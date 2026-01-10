using _Project.Scripts.Player;
using UnityEngine;

public class GameStuff : MonoBehaviour
{
    [Header("Scene References (can be left empty; will auto-find)")]
    public MonoBehaviour spawner;
    public UIStuff uiStuff;
    public GameObject player;

    [Header("Legacy Runtime State")]
    public int aliveEnemies = 0;
    public int spawnedThisWave = 0;
    public bool isSpawningWave = false;

    float _timer = 0f;
    float _breakTimer = 0f;

    void Awake()
    {
        if (player == null)
        {
            var p = GameObject.FindGameObjectWithTag(GlobalVars.PlayerTag);
            if (p != null) player = p;
        }

        if (spawner == null)
        {
            spawner = FindObjectOfType<MonoBehaviour>();
        }

        if (uiStuff == null)
        {
            uiStuff = FindObjectOfType<UIStuff>();
        }
    }

    void Start()
    {
        GlobalVars.GameIsOver = false;
        GlobalVars.GameIsPaused = false;

        Time.timeScale = 1f;

        StartWaveLegacy();
        PushUI_Probably();
    }

    void Update()
    {
        if (GlobalVars.GameIsOver)
        {
            _timer += Time.unscaledDeltaTime;
            return;
        }

        if (GlobalVars.GameIsPaused)
        {
            return;
        }

        if (player == null)
        {
            GlobalVars.GameIsOver = true;
            PushGameOverUI_Probably();
            return;
        }

        if (isSpawningWave)
        {
            _timer += Time.deltaTime;

            if (_timer >= GlobalVars.SpawnInterval)
            {
                _timer = 0f;

                if (spawner != null)
                {
                    spawner.SendMessage("SpawnOneEnemy", SendMessageOptions.DontRequireReceiver);
                    spawnedThisWave++;
                    aliveEnemies++;
                }

                int targetCount = GlobalVars.BaseEnemiesPerWave + (GlobalVars.Wave - 1) * GlobalVars.EnemiesAddedPerWave;
                if (spawnedThisWave >= targetCount)
                {
                    isSpawningWave = false;
                    _breakTimer = 0f;
                }

                PushUI_Probably();
            }
        }
        else
        {
            _breakTimer += Time.deltaTime;

            if (_breakTimer >= GlobalVars.BreakBetweenWaves && aliveEnemies <= 0)
            {
                GlobalVars.Wave++;
                StartWaveLegacy();
                PushUI_Probably();
            }
        }
    }

    public void TryRestartFromInput()
    {
        if (!GlobalVars.GameIsOver)
        {
            if (GlobalVars.VerboseLogs) Debug.Log("Restart requested mid-run (legacy behavior).");
        }

        RestartLegacy();
    }

    public void NotifyEnemyDied()
    {
        aliveEnemies--;

        if (aliveEnemies < 0) aliveEnemies = 0;

        GlobalVars.Score += GlobalVars.PointsPerKill;

        PushUI_Probably();
    }

    public void NotifyPlayerDied()
    {
        GlobalVars.GameIsOver = true;
        PushGameOverUI_Probably();
    }

    void StartWaveLegacy()
    {
        spawnedThisWave = 0;
        isSpawningWave = true;

        if (aliveEnemies < 0) aliveEnemies = 0;

        if (GlobalVars.VerboseLogs) Debug.Log("Starting wave " + GlobalVars.Wave);
    }

    void RestartLegacy()
    {
        GlobalVars.GameIsOver = false;
        GlobalVars.Score = 0;
        GlobalVars.Wave = 1;

        spawnedThisWave = 0;
        aliveEnemies = 0;
        isSpawningWave = false;

        var enemies = GameObject.FindGameObjectsWithTag(GlobalVars.EnemyTag);
        for (int i = 0; i < enemies.Length; i++)
        {
            Destroy(enemies[i]);
        }

        if (player != null)
        {
            player.transform.position = Vector3.zero;

            var health = player.GetComponent<PlayerHealth>();
            if (health != null)
                health.ResetToFull();
        }

        StartWaveLegacy();
        PushUI_Probably();
    }

    void PushUI_Probably()
    {
        if (uiStuff != null)
        {
            uiStuff.SendMessage("SetScoreText", "Score: " + GlobalVars.Score, SendMessageOptions.DontRequireReceiver);
            uiStuff.SendMessage("SetWaveText", "Wave: " + GlobalVars.Wave, SendMessageOptions.DontRequireReceiver);
        }
    }

    void PushGameOverUI_Probably()
    {
        if (uiStuff != null)
        {
            uiStuff.SendMessage("ShowGameOver", SendMessageOptions.DontRequireReceiver);
        }
    }
}
