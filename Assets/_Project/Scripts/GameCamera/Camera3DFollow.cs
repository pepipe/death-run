using UnityEngine;

namespace pepipe.DeathRun.GameCamera
{
    public class Camera3DFollow : MonoBehaviour
    {
        [SerializeField] Transform m_FollowTransform;

        Vector3 _offset;
    
        void Start() {
            _offset = transform.position - m_FollowTransform.transform.position;
        }
    
        void LateUpdate() {
            transform.position = m_FollowTransform.transform.position + _offset;
        }
    }
}