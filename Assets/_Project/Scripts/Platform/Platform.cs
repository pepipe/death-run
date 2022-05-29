using System.Collections.Generic;
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
        [SerializeField] List<float> m_ObstaclesPosition;
        
        public PlatformPieces NumberOfPieces => m_NumberOfPieces;
        
        ObstaclesPool _pool;
        readonly List<Obstacle> _spawnedObstacles = new();
        
        public void Activate() {
            Utils.RandomCollection.Shuffle(m_ObstaclesPosition);
            gameObject.SetActive(true);
        }

        public void Reset() {
            gameObject.SetActive(false);
            foreach (var obstacle in _spawnedObstacles) {
                _pool.Release(obstacle);
            }
        }

        public void SpawnObstacle(ObstaclesPool pool) {
            if (m_ObstaclesPosition.Count == 0) return;

            _pool = pool;
            var numberOfObstacles = Random.Range(0, m_ObstaclesPosition.Count + 1);
            for (var i = 0; i < numberOfObstacles; ++i) { 
                var obstacle = pool.Allocate(true);
                obstacle.transform.parent = transform;
                obstacle.transform.localPosition = new Vector3(
                    m_ObstaclesPosition[i],
                    0,
                    0);
                _spawnedObstacles.Add(obstacle);
            }
        }
    }
}
