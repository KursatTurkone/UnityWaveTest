using _Project.Scripts.Core;
using UnityEngine;
using UnityEngine.UI;

namespace _Project.Scripts.UI
{
    public class GameOverPanel : MonoBehaviour
    {
        [Header("UI Elements")]
        [SerializeField] private GameObject root;
        [SerializeField] private Button restartButton;
        [SerializeField] private Text hintText;

        private void Awake()
        {
            if (root == null)
                root = gameObject;

            if (restartButton != null)
            {
                restartButton.onClick.AddListener(OnRestartClicked);
            }
        }

        private void Start()
        {
            if (hintText != null)
            {
                hintText.text = $"Press 'R' to restart";
            }
        }

        public void Show()
        {
            if (root != null)
                root.SetActive(true);
        }

        public void Hide()
        {
            if (root != null)
                root.SetActive(false);
        }

        private void OnRestartClicked()
        {
            EventBus.Instance.Publish_GameRestart();
            Hide();
        }
    }
}
