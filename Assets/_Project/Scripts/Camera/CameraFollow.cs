using UnityEngine;

namespace pepipe.DeathRun.Camera
{
    public class CameraFollow : MonoBehaviour
    {

        [SerializeField] Transform m_FollowTransform;
        [SerializeField] bool m_FollowInY;

        Transform _originalCamera;
    
        void Start() {
            _originalCamera = transform;
        }
    
        void LateUpdate() {
            var yPos = m_FollowInY
                ? m_FollowTransform.position.y
                : _originalCamera.position.y;
            var cameraTransform = transform;
            cameraTransform.position = new Vector3(m_FollowTransform.position.x, yPos, cameraTransform.position.z);
        }
    }
}