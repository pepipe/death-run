using UnityEngine;

namespace pepipe.DeathRun.Road
{
    public class WheelsRotator : MonoBehaviour {
        [SerializeField] float m_RotateSpeed = 10f;
        [SerializeField] Vehicle m_Vehicle;
        void Update() {
            transform.Rotate(m_RotateSpeed * m_Vehicle.VehicleSpeed * Time.deltaTime, 0, 0);;
        }
    }
}
