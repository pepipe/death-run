using TMPro;
using UnityEngine;

namespace pepipe.DeathRun.UI
{
    public class UIController : MonoBehaviour {
        [SerializeField] TextMeshProUGUI m_ScoreText;

        SceneController _sceneController;

        void Start() {
            _sceneController = FindObjectOfType<SceneController>();
            _sceneController.Player.Score += UpdateScoreText;
        }

        void OnDisable() {
            _sceneController.Player.Score -= UpdateScoreText;
        }

        void UpdateScoreText(int playerPos) {
            m_ScoreText.text = playerPos + "m";
        }
    }
}
