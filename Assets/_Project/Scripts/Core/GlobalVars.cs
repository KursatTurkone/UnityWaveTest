using UnityEngine;

public static class GlobalVars
{
    // Game loop-ish
    public static bool GameIsOver = false;
    public static bool GameIsPaused = false;

    // Scoring
    public static int Score = 0;
    public static int PointsPerKill = 10;

    // "Wave" (poorly defined here on purpose)
    public static int Wave = 1;
    public static int BaseEnemiesPerWave = 5;
    public static int EnemiesAddedPerWave = 3;

    // Spawning
    public static float SpawnInterval = 0.5f;
    public static float BreakBetweenWaves = 3.0f;

    // Restart
    public static KeyCode RestartKey = KeyCode.R;

    // Debug toggles
    public static bool VerboseLogs = false;

    public static string PlayerTag = "Player";
    public static string EnemyTag = "Enemy";
    public static string BulletTag = "Bullet";
}
