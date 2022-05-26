using UnityEngine;

namespace pepipe.DeathRun
{
    public class GameManager : MonoBehaviour
    {
        #region Singleton
        public static GameManager Instance { get; private set; }

        void Awake()
        {
            if (Instance != null && Instance != this)
                Destroy(gameObject);
            else
                Instance = this;

            DontDestroyOnLoad(gameObject);
        }
        #endregion
    }
}
