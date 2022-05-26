using System.Collections.Generic;
using pepipe.DeathRun.Score;
using pepipe.Utils.Logging;
using TMPro;
using UnityEngine;

namespace pepipe.DeathRun
{
    public class MenuUIController : MonoBehaviour
    {
        [SerializeField] List<TextMeshProUGUI> m_HighScores;

        [Header("Debug")]
        [SerializeField] CustomLogger m_Logger;

        void Start()
        {
            var scores = Leaderboard.Entries;
            //empty scores hide leaderboard panel
            if(string.IsNullOrEmpty(scores[0].ScoreName))
               m_HighScores[0].transform.parent.gameObject.SetActive(false);
            
            for (var i = 0; i < m_HighScores.Count; ++i)
            {
                if (!string.IsNullOrEmpty(scores[i].ScoreName))
                    m_HighScores[i].text = $"{scores[i].ScoreName}: {scores[i].Score}";
                else
                    m_HighScores[i].gameObject.SetActive(false);
            }
        }
    }
}
