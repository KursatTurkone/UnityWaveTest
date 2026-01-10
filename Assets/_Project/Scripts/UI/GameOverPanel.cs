using UnityEngine;
using UnityEngine.UI;

public class GameOverPanel : MonoBehaviour
{
    [Header("UI")]
    public GameObject root;
    public Button restartButton;
    public Text hintText;

    [Header("Legacy")]
    public bool autoWire = true;

    GameStuff _game;

    void Awake()
    {
        if (autoWire)
        {
            if (root == null) root = gameObject;

            if (restartButton == null)
            {
                var t = transform.Find("RestartButton");
                if (t != null) restartButton = t.GetComponent<Button>();
            }

            if (hintText == null)
            {
                var t = transform.Find("HintText");
                if (t != null) hintText = t.GetComponent<Text>();
            }
        }

        _game = FindObjectOfType<GameStuff>();

        if (restartButton != null)
        {
            restartButton.onClick.AddListener(RestartClicked);
        }
    }

    void Update()
    {
        if (root != null && root.activeSelf && Input.GetKeyDown(GlobalVars.RestartKey))
        {
            RestartClicked();
        }

        if (hintText != null)
        {
            hintText.text = "Press '" + GlobalVars.RestartKey + "' to restart";
        }
    }

    public void Show()
    {
        if (root != null) root.SetActive(true);
    }

    public void Hide()
    {
        if (root != null) root.SetActive(false);
    }

    void RestartClicked()
    {
        if (_game != null)
        {
            _game.TryRestartFromInput();
        }
        else
        {
            GlobalVars.GameIsOver = false;
            GlobalVars.Score = 0;
            GlobalVars.Wave = 1;
        }

        Hide();
    }
}
