using pepipe.DeathRun.Player;
using UnityEngine;

namespace pepipe.DeathRun
{
    public class SceneController : MonoBehaviour {
        [SerializeField] PlayerController m_Player;
        [SerializeField] string m_Version = "0.0.01";

        public PlayerController Player => m_Player;
        public string Version => m_Version;
    }
}
