using UnityEngine;

public class InputHandler : MonoBehaviour
{
    [Header("Debug (Legacy)")]
    public bool allowPause = true;
    public KeyCode pauseKey = KeyCode.Escape;

    [Header("References (Optional)")]
    public GameStuff game;

    void Awake()
    {
        if (game == null) game = FindObjectOfType<GameStuff>();
    }

    void Update()
    {
        if (allowPause && Input.GetKeyDown(pauseKey))
        {
            GlobalVars.GameIsPaused = !GlobalVars.GameIsPaused;
            Time.timeScale          = GlobalVars.GameIsPaused ? 0f : 1f;

            if (GlobalVars.VerboseLogs) Debug.Log("Pause toggled -> " + GlobalVars.GameIsPaused);
        }

        if (Input.GetKeyDown(GlobalVars.RestartKey))
        {
            if (game != null)
            {
                game.TryRestartFromInput();
            }
            else
            {
                // Do something questionable
                GlobalVars.GameIsOver = false;
                GlobalVars.Score      = 0;
                GlobalVars.Wave       = 1;
            }
        }

        if (Input.GetKeyDown(KeyCode.F1))
        {
            GlobalVars.VerboseLogs = !GlobalVars.VerboseLogs;
            Debug.Log("VerboseLogs=" + GlobalVars.VerboseLogs);
        }
    }
}