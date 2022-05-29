using System;
using com.pepipe.Pool;
using UnityEngine;
using Random = UnityEngine.Random;

namespace pepipe.DeathRun.Road
{
    public class Vehicle : MonoBehaviour, IResettable {
        public Action<Vehicle> DestroyVehicle;
        public enum VehicleType {
            Cop,
            NormalCar,
            NormalCar2,
            SportsCar,
            SportsCar2,
            Suv,
            Taxi
        }
        
        [SerializeField] Vector2 m_MinMaxSpeed;
        [SerializeField] VehicleType m_Type;
        [SerializeField] bool m_InitialCar;
        
        public float VehicleSpeed => _vehicleSpeed;
        public VehicleType Type => m_Type;

        float _vehicleSpeed;
        Vector3 _originalPosition;

        void Start() {
            _originalPosition = transform.position;
            _vehicleSpeed = Random.Range(m_MinMaxSpeed.x, m_MinMaxSpeed.y);
        }

        void Update() {
            transform.position = new Vector3(_originalPosition.x, 
                _originalPosition.y,
                transform.position.z + _vehicleSpeed * Time.deltaTime);
        }

        void OnTriggerEnter(Collider other) {
            if (!other.tag.Equals(GameManager.RoadSpawnerTag) || other.name.Equals("Spawn")) return;
            
            if(!m_InitialCar)
                DestroyVehicle?.Invoke(this);
            else
                Destroy(gameObject);
                
        }

        public void Activate() {
            gameObject.SetActive(true);
        }

        public void Reset() {
            gameObject.SetActive(false);
        }
    }
}
