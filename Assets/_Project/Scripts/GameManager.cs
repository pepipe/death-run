using UnityEngine;

namespace pepipe.DeathRun
{
    public class GameManager : MonoBehaviour {
        [SerializeField] bool m_DestroyOtherInstance;
        #region Singleton
        public static GameManager Instance { get; private set; }

        void Awake()
        {
            if (Instance != null && Instance != this && !m_DestroyOtherInstance)
                Destroy(gameObject);
            else if (Instance != null && Instance != this) {
                Destroy(Instance.gameObject);
                Instance = this;
            }
            else
                Instance = this;

            DontDestroyOnLoad(gameObject);
        }
        #endregion
        
        #region LayerNames
        public const string GroundLayer = "Ground";
        public const string FallLayer = "Fall";
        public const string DeathLayer = "Death";
        #endregion

        #region TagNames
        public const string PlayerTag = "Player";
        public const string RoadSpawnerTag = "RoadSpawner";
        public const string ObstacleTag = "Obstacle";
        public const string ObstacleDespawnerTag = "ObstacleDespawner";
        #endregion
        
        public int Score { get; set; }
    }
}
