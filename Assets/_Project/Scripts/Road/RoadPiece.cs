using com.pepipe.Pool;
using UnityEngine;

namespace pepipe.DeathRun
{
    public class RoadPiece : MonoBehaviour, IResettable {
        [SerializeField] GameObject m_CarSpawn;

        public GameObject CarSpawn => m_CarSpawn;
        
        public void Activate() {
            gameObject.SetActive(true);
        }

        public void Reset() {
            gameObject.SetActive(false);
        }
    }
}
