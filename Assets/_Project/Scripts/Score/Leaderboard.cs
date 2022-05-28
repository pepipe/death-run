using UnityEngine;
using System.Collections.Generic;
using System.Linq;

namespace pepipe.DeathRun.Score
{
    public static class Leaderboard {
        public struct ScoreEntry
        {
            public string ScoreName;
            public int Score;

            public ScoreEntry(string scoreName, int score)
            {
                ScoreName = scoreName;
                Score = score;
            }
        }
        
        public const int EntryCount = 8;
        
        public static List<ScoreEntry> Entries
        {
            get
            {
                if (_entries != null) return _entries;
                
                _entries = new List<ScoreEntry>();
                LoadScores();

                return _entries;
            }
        }

        public static int GetMinScore()
        {
            return _entries.Select(e => e.Score).Min();
        }

        static List<ScoreEntry> _entries;

        const string PlayerPrefsBaseKey = "leaderboard";

        public static ScoreEntry GetEntry(int index)
        {
            return Entries[index];
        }

        public static void Record(string name, int score)
        {
            Entries.Add(new ScoreEntry(name, score));
            SortScores();
            Entries.RemoveAt(Entries.Count - 1);
            SaveScores();
        }

        static void SortScores()
        {
            _entries.Sort((a, b) => b.Score.CompareTo(a.Score));
        }

        static void LoadScores()
        {
            _entries.Clear();

            for (var i = 0; i < EntryCount; ++i)
            {
                ScoreEntry entry;
                entry.ScoreName = PlayerPrefs.GetString(PlayerPrefsBaseKey + "[" + i + "].name", "");
                entry.Score = PlayerPrefs.GetInt(PlayerPrefsBaseKey + "[" + i + "].score", 0);
                _entries.Add(entry);
            }

            SortScores();
        }

        static void SaveScores()
        {
            for (var i = 0; i < EntryCount; ++i)
            {
                var entry = _entries[i];
                PlayerPrefs.SetString(PlayerPrefsBaseKey + "[" + i + "].name", entry.ScoreName);
                PlayerPrefs.SetInt(PlayerPrefsBaseKey + "[" + i + "].score", entry.Score);
            }
        }
    }
}