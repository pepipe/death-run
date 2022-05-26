using pepipe.DeathRun.Player;
using UnityEngine;

namespace pepipe.DeathRun
{
    public class SceneController : MonoBehaviour {
        [SerializeField] PlayerController m_Player;

        public PlayerController Player => m_Player;
    }
}
