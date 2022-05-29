using UnityEngine;

namespace pepipe.DeathRun.Player
{
    [RequireComponent(typeof(AudioSource))]
    public class PlayerSounds : MonoBehaviour
    {
        [SerializeField] SceneController _sceneController;
        [SerializeField] AudioClip m_DeathSound;
        
        AudioSource _audioSource;

        void Awake()
        {
            _audioSource = GetComponent<AudioSource>();
        }

        void Start()
        {
            _sceneController.Player.Dying += OnDeath;
        }

        void OnDisable()
        {
            _sceneController.Player.Dying -= OnDeath;
        }

        void OnDeath()
        {
            _audioSource.clip = m_DeathSound;
            _audioSource.Play();
        }
    }
}
