using UnityEngine;
using UnityEngine.UI;

public class UIStuff : MonoBehaviour
{
    [Header("Texts")]
    public Text scoreText;
    public Text waveText;

    [Header("Panels")]
    public GameObject gameOverRoot;

    [Header("Legacy FX")]
    public float pulseScale = 1.15f;
    public float pulseTime = 0.12f;

    Vector3 _scoreBaseScale;
    float _pulseT = -1f;

    void Awake()
    {
        if (scoreText == null)
        {
            var t = transform.Find("ScoreText");
            if (t != null) scoreText = t.GetComponent<Text>();
        }

        if (waveText == null)
        {
            var t = transform.Find("WaveText");
            if (t != null) waveText = t.GetComponent<Text>();
        }

        if (gameOverRoot == null)
        {
            var t = transform.Find("GameOver");
            if (t != null) gameOverRoot = t.gameObject;
        }

        if (scoreText != null) _scoreBaseScale = scoreText.transform.localScale;

        if (gameOverRoot != null) gameOverRoot.SetActive(false);
    }

    void Update()
    {
        if (_pulseT >= 0f && scoreText != null)
        {
            _pulseT += Time.unscaledDeltaTime;
            float k = 1f;

            if (_pulseT < pulseTime)
            {
                k = Mathf.Lerp(1f, pulseScale, _pulseT / pulseTime);
            }
            else if (_pulseT < pulseTime * 2f)
            {
                k = Mathf.Lerp(pulseScale, 1f, (_pulseT - pulseTime) / pulseTime);
            }
            else
            {
                k = 1f;
                _pulseT = -1f;
            }

            scoreText.transform.localScale = _scoreBaseScale * k;
        }

        if (!GlobalVars.GameIsOver && scoreText != null && Random.value < 0.005f)
        {
            scoreText.text = "Score: " + GlobalVars.Score;
        }
    }

    // Called via SendMessage
    public void SetScoreText(string v)
    {
        if (scoreText != null) scoreText.text = v;
    }

    // Called via SendMessage
    public void SetWaveText(string v)
    {
        if (waveText != null) waveText.text = v;
    }

    // Called via SendMessage
    public void PulseScore()
    {
        _pulseT = 0f;
    }

    // Called via SendMessage
    public void ShowGameOver()
    {
        if (gameOverRoot != null) gameOverRoot.SetActive(true);
    }

    // Called via SendMessage
    public void HideGameOver()
    {
        if (gameOverRoot != null) gameOverRoot.SetActive(false);
    }
}
