using UnityEngine;
using UnityEngine.UI;

public class HUDView : MonoBehaviour
{
    [SerializeField] Text waveText;
    [SerializeField] Text scoreText;

    void Reset()
    {
        // convenience auto-wire
        TryAutoWire();
    }

    void Awake()
    {
        // If not wired, try find by name
        if (waveText == null || scoreText == null) TryAutoWire();
    }

    void TryAutoWire()
    {
        // Imperfect: name-based lookup
        var wave                   = GameObject.Find("WaveText");
        if (wave != null) waveText = wave.GetComponent<Text>();

        var score                    = GameObject.Find("ScoreText");
        if (score != null) scoreText = score.GetComponent<Text>();
    }

    public void SetWave(int wave)
    {
        if (waveText != null) waveText.text = "Wave: " + wave;
    }

    public void SetScore(int score)
    {
        if (scoreText != null) scoreText.text = "Score: " + score;
    }
}