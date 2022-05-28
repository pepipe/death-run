using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace pepipe.DeathRun
{
    public class VehicleSpawner : MonoBehaviour {
        [SerializeField] int m_MaxVehiclesSpawned = 12;
        [SerializeField] float m_SpawnTimerMin = 1.5f;
        [SerializeField] float m_SpawnTimerMax = 4f;
        [SerializeField] RoadSpawner m_RoadSpawner;
        [SerializeField] List<GameObject> m_Lanes;
        [Header("Pools")]
        [SerializeField] VehiclesPool m_CopsPool;
        [SerializeField] VehiclesPool m_NormalCarsPool;
        [SerializeField] VehiclesPool m_NormalCars2Pool;
        [SerializeField] VehiclesPool m_SportCarsPool;
        [SerializeField] VehiclesPool m_SportCars2Pool;
        [SerializeField] VehiclesPool m_SuvsPool;
        [SerializeField] VehiclesPool m_TaxisPool;

        List<VehiclesPool> _vehiclesPool;
        int _vehiclesSpawned;

        const int VehiclesPoolsNumber = 7;
        void Start() {
            _vehiclesPool = new List<VehiclesPool> {
                m_CopsPool,
                m_NormalCarsPool,
                m_NormalCars2Pool,
                m_SportCarsPool,
                m_SportCars2Pool,
                m_SuvsPool,
                m_TaxisPool
            };
            
            StartCoroutine(SpawningVehicles());
        }

        IEnumerator SpawningVehicles() {
            yield return new WaitForSeconds(Random.Range(m_SpawnTimerMin, m_SpawnTimerMax));
            SpawnVehicle();
            StartCoroutine(SpawningVehicles());
        }

        void SpawnVehicle() {
            if (_vehiclesSpawned >= m_MaxVehiclesSpawned) return;

            ++_vehiclesSpawned;
            var pool = _vehiclesPool[Random.Range(0, VehiclesPoolsNumber)];
            var vehicle = pool.Allocate(false);
            var vehiclePosition = vehicle.transform.position;

            var lane = m_Lanes[Random.Range(0, m_Lanes.Count)];
            vehicle.transform.position = new Vector3(
                lane.transform.position.x,
                vehiclePosition.y,
                m_RoadSpawner.CurrentRoadPiece.CarSpawn.transform.position.z);
            vehicle.DestroyVehicle += OnVehicleDestroy;
            vehicle.transform.parent = lane.transform;
            vehicle.Activate();
        }

        void OnVehicleDestroy(Vehicle vehicle) {
            vehicle.DestroyVehicle -= OnVehicleDestroy;
            --_vehiclesSpawned;
            switch (vehicle.Type) {
                case Vehicle.VehicleType.Suv:
                    m_SuvsPool.Release(vehicle);
                    break;
                case Vehicle.VehicleType.Taxi:
                    m_TaxisPool.Release(vehicle);
                    break;
                case Vehicle.VehicleType.NormalCar:
                    m_NormalCarsPool.Release(vehicle);
                    break;
                case Vehicle.VehicleType.NormalCar2:
                    m_NormalCars2Pool.Release(vehicle);
                    break;
                case Vehicle.VehicleType.SportsCar:
                    m_SportCarsPool.Release(vehicle);
                    break;
                case Vehicle.VehicleType.SportsCar2:
                    m_SportCars2Pool.Release(vehicle);
                    break;
                case Vehicle.VehicleType.Cop:
                default:
                    m_CopsPool.Release(vehicle);
                    break;
            }
        }
    }
}
