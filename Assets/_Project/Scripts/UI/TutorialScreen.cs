using pepipe.Utils.Logging;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

namespace pepipe.DeathRun.UI
{
    public class TutorialScreen : MonoBehaviour {
        [SerializeField] AudioSource m_Music;
        
        [Header("Debug")] 
        [SerializeField] CustomLogger m_Logger;
        
        bool _buttonPressed;
        
        void Awake() {
            Time.timeScale = 0;
        }

        void Start() {
            InputSystem.onAnyButtonPress.CallOnce(ctrl => {
                m_Logger.Log($"{ctrl} pressed", this);
                m_Music.Play();
                gameObject.SetActive(false);
                Time.timeScale = 1;
            });
        }
    }
}
