using pepipe.DeathRun.Player;
using UnityEngine;

namespace pepipe.DeathRun
{
    public class SceneController : MonoBehaviour {
        [SerializeField] PlayerController m_Player;
        [SerializeField] Player3DController m_3DPlayer;

        public IController Player => m_Player != null ? m_Player : m_3DPlayer;
    }
}
