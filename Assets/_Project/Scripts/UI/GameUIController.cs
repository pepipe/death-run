using System.Collections;
using pepipe.DeathRun.Score;
using pepipe.Utils.Logging;
using TMPro;
using UnityEngine;

namespace pepipe.DeathRun.UI
{
    public class GameUIController : MonoBehaviour {
        [SerializeField] SceneLoader m_SceneLoader;
        [Header("Gameplay UI")]
        [SerializeField] TextMeshProUGUI m_ScoreText;
        [SerializeField] bool m_IsEndScene;

        [Header("GameOver Panel")] 
        [SerializeField] GameObject m_GameOverPanel;
        [SerializeField] TextMeshProUGUI m_GameOverMessageText;
        [SerializeField] TextMeshProUGUI m_GameOverScoreText;
        [SerializeField] GameObject m_AddScore;
        [SerializeField] TMP_InputField m_AddScoreInputField;
        [SerializeField] TextMeshProUGUI m_StatusText;

        [Header("Debug")]
        [SerializeField] CustomLogger m_Logger;
        
        SceneController _sceneController;
        int _statusCounter;

        void Start() {
            _sceneController = FindObjectOfType<SceneController>();
            _sceneController.Player.Score += UpdateScoreText;
            _sceneController.Player.Dying += OnPlayerDead;

            UpdateScoreText(0);
            m_GameOverPanel.SetActive(false);
            m_GameOverMessageText.gameObject.SetActive(false);
            m_AddScore.SetActive(false);
        }

        void OnDisable() {
            if (_sceneController == null || _sceneController.Player == null) return;
            
            _sceneController.Player.Score -= UpdateScoreText;
            _sceneController.Player.Dying -= OnPlayerDead;
        }

        public void SavePlayerScore()
        {
            if (string.IsNullOrEmpty(m_AddScoreInputField.text)) return;
            
            var playerScore = int.Parse(m_ScoreText.text.Remove(m_ScoreText.text.Length - 1));
            Leaderboard.Record(m_AddScoreInputField.text, playerScore);
        }
        
        public void DeleteAllRecords()
        {
            PlayerPrefs.DeleteAll();
        }

        void UpdateScoreText(int playerPos) {
            var score = GameManager.Instance.Score + playerPos;
            m_ScoreText.text = score + "m";
        }

        void OnPlayerDead()
        {
            m_GameOverScoreText.text = m_ScoreText.text;
            var playerScore = int.Parse(m_ScoreText.text.Remove(m_ScoreText.text.Length - 1));
            if (m_IsEndScene)
                SetupHighScore(playerScore);
            else
                DeathIsOnlyTheBeginning(playerScore);

            m_GameOverPanel.SetActive(true);
        }

        void DeathIsOnlyTheBeginning(int playerScore) {
            GameManager.Instance.Score = playerScore;
            _sceneController.Player.Dying -= OnPlayerDead;
            StartCoroutine(LoadNextLevel());
        }

        IEnumerator LoadNextLevel() {
            while (_statusCounter < 3) {
                yield return new WaitForSeconds(1f);
                ++_statusCounter;
                m_StatusText.text = _statusCounter.ToString();
            }
            m_SceneLoader.LoadScene();
        }

        void SetupHighScore(int playerScore)
        {
            var minScore = Leaderboard.GetMinScore();
            m_Logger.Log($"Min score stored: {minScore}", this);
            m_Logger.Log($"Player Score: {playerScore}", this);
            if (playerScore > minScore)
            {
                //TODO: show message highscore
                m_AddScore.SetActive(true);
            }
            else
            {
                //TODO: show message didn't get highscore
            }
        }
    }
}
