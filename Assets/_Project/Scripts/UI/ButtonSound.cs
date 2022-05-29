using UnityEngine;

namespace pepipe.DeathRun.UI
{
    [RequireComponent(typeof(AudioSource))]
    public class ButtonSound : MonoBehaviour {
        [SerializeField] AudioClip m_ButtonSound;
        
        AudioSource _audioSource;

        void Awake() {
            _audioSource = GetComponent<AudioSource>();
        }

        void Start() {
            _audioSource.clip = m_ButtonSound;
        }

        public void PlaySound() {
            _audioSource.Play();
        }
    }
}
