using UnityEngine;

namespace pepipe.DeathRun
{
    public class PlayerFollow : MonoBehaviour {
        [SerializeField] Transform m_Target;

        void Update() {
            var objectTransform = transform;
            var position = objectTransform.position;
            position = new Vector3(m_Target.position.x, position.y, position.z);
            objectTransform.position = position;
        }
    }
}
