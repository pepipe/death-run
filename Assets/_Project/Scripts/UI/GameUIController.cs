using System;
using pepipe.DeathRun.Score;
using pepipe.Utils.Logging;
using TMPro;
using UnityEngine;

namespace pepipe.DeathRun.UI
{
    public class GameUIController : MonoBehaviour {
        [Header("Gameplay UI")]
        [SerializeField] TextMeshProUGUI m_ScoreText;

        [Header("GameOver Panel")] 
        [SerializeField] GameObject m_GameOverPanel;
        [SerializeField] TextMeshProUGUI m_GameOverMessageText;
        [SerializeField] TextMeshProUGUI m_GameOverScoreText;
        [SerializeField] GameObject m_AddScore;
        [SerializeField] TMP_InputField m_AddScoreInputField;

        [Header("Debug")]
        [SerializeField] CustomLogger m_Logger;
        
        SceneController _sceneController;

        void Start() {
            _sceneController = FindObjectOfType<SceneController>();
            _sceneController.Player.Score += UpdateScoreText;
            _sceneController.Player.Dying += OnPlayerDead;

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
            m_ScoreText.text = playerPos + "m";
        }

        void OnPlayerDead()
        {
            m_GameOverScoreText.text = m_ScoreText.text;
            SetupHighScore();

            m_GameOverPanel.SetActive(true);
        }

        void SetupHighScore()
        {
            var minScore = Leaderboard.GetMinScore();
            var playerScore = int.Parse(m_ScoreText.text.Remove(m_ScoreText.text.Length - 1));
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
