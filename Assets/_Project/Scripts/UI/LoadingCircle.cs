using UnityEngine;

namespace pepipe.DeathRun.UI
{
    [RequireComponent(typeof(RectTransform))]
    public class LoadingCircle : MonoBehaviour
    {
        [SerializeField] float m_RotateSpeed = 200f;

        RectTransform _rectComponent;

        void Start()
        {
            _rectComponent = GetComponent<RectTransform>();
        }

        void Update()
        {
            _rectComponent.Rotate(0f, 0f, -m_RotateSpeed * Time.deltaTime);
        }
    }
}