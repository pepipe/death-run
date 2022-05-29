using UnityEngine;

namespace pepipe.DeathRun.Player
{
    public class PlayerFollow : MonoBehaviour {
        [SerializeField] Transform m_Target;
        [SerializeField] bool m_FollowInX;
        [SerializeField] bool m_FollowInY;
        [SerializeField] bool m_FollowInZ;

        Vector3 _objectPosition;

        void Start() {
            _objectPosition = transform.position;
        }

        void Update() {
            var targetPosition = m_Target.position;
            transform.position = new Vector3(
                m_FollowInX ? targetPosition.x : _objectPosition.x,
                m_FollowInY ? targetPosition.y : _objectPosition.y,
                m_FollowInZ ? targetPosition.z : _objectPosition.z
            );
        }
    }
}
