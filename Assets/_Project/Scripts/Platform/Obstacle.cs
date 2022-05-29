using com.pepipe.Pool;
using UnityEngine;

namespace pepipe.DeathRun
{
    public class Obstacle : MonoBehaviour, IResettable
    {
        public void Activate() {
            gameObject.SetActive(true);
        }

        public void Reset() {
            gameObject.SetActive(false);
        }
    }
}
