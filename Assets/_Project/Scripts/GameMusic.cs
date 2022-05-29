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
            if(m_sceneController != null && m_sceneController.Player != null)
                m_sceneController.Player.Dying -= OnDeath;
        }

        void OnDeath()
        {
            m_MusicAudioSource.Stop();
        }
    }
}
