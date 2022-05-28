using UnityEngine;

namespace pepipe.DeathRun
{
    public class GameMusic : MonoBehaviour
    {
        [SerializeField] SceneController m_sceneController;
        [SerializeField] AudioSource m_MusicAudioSource;

        void Start()
        {
            m_sceneController.Player.Dying += OnDeath;
        }

        void OnDisable()
        {
            m_sceneController.Player.Dying -= OnDeath;
        }

        void OnDeath()
        {
            m_MusicAudioSource.Stop();
        }
    }
}
