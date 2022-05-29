using com.pepipe.Pool;
using UnityEngine;

namespace pepipe.DeathRun.Platform
{
    public class Platform : MonoBehaviour, IResettable
    {
        public enum PlatformPieces {
            Pieces2 = 2,
            Pieces3 = 3,
            Pieces4 = 4,
            Pieces6 = 6,
            Pieces8 = 8,
            Pieces10 = 10
        }

        [SerializeField] PlatformPieces m_NumberOfPieces = PlatformPieces.Pieces2;

        public PlatformPieces NumberOfPieces => m_NumberOfPieces;
        
        public void Activate() {
            gameObject.SetActive(true);
        }

        public void Reset() {
            gameObject.SetActive(false);
        }
    }
}
