using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace pepipe.DeathRun.UI
{
    public class UIController : MonoBehaviour {
        [SerializeField] TextMeshProUGUI m_ScoreText;
        [SerializeField] Button m_RestartBtn;
        [SerializeField] TextMeshProUGUI m_Version;

        SceneController _sceneController;

        void Start() {
            _sceneController = FindObjectOfType<SceneController>();
            _sceneController.Player.Score += UpdateScoreText;
            _sceneController.Player.Dying += OnPlayerDead;
            m_Version.text = _sceneController.Version;
        }

        void OnDisable() {
            if (_sceneController == null || _sceneController.Player == null) return;
            
            _sceneController.Player.Score -= UpdateScoreText;
            _sceneController.Player.Dying -= OnPlayerDead;
        }

        void UpdateScoreText(int playerPos) {
            m_ScoreText.text = playerPos + "m";
        }

        void OnPlayerDead() {
            m_RestartBtn.onClick.AddListener(() => {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            });
            m_RestartBtn.gameObject.SetActive(true);
        }
    }
}
