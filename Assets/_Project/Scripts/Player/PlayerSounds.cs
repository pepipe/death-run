using UnityEngine;

namespace pepipe.DeathRun.Player
{
    [RequireComponent(typeof(AudioSource))]
    public class PlayerSounds : MonoBehaviour
    {
        [SerializeField] SceneController _sceneController;
        [SerializeField] AudioClip m_JumpSound;
        [SerializeField] AudioClip m_DeathSound;
        
        AudioSource _audioSource;

        void Awake()
        {
            _audioSource = GetComponent<AudioSource>();
        }

        void Start() {
            _sceneController.Player.Jumping += OnJump;
            _sceneController.Player.Dying += OnDeath;
        }

        void OnDisable()
        {
            _sceneController.Player.Jumping -= OnJump;
            _sceneController.Player.Dying -= OnDeath;
        }

        void OnJump() {
            _audioSource.clip = m_JumpSound;
            _audioSource.Play();
        }

        void OnDeath()
        {
            _audioSource.clip = m_DeathSound;
            _audioSource.Play();
        }
    }
}
