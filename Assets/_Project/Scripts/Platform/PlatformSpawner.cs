using System.Collections.Generic;
using pepipe.Utils.Logging;
using UnityEngine;

namespace pepipe.DeathRun
{
    public class PlatformSpawner : MonoBehaviour {
        [Header("Camera Settings")]
        [SerializeField] Transform m_MainCamera;
        [SerializeField] int m_DistanceToSpawn = 15;
        
        [Header("Spawner Settings")] 
        [SerializeField] int m_MinGap = 5;
        [SerializeField] int m_MaxGap = 12;
        [SerializeField] Platform m_InitialPlatform;
        
        [Header("Pool References")]
        [SerializeField] PlatformPiecesPool m_Platform2PiecesPool;
        [SerializeField] PlatformPiecesPool m_Platform3PiecesPool;
        [SerializeField] PlatformPiecesPool m_Platform4PiecesPool;
        [SerializeField] PlatformPiecesPool m_Platform6PiecesPool;
        [SerializeField] PlatformPiecesPool m_Platform8PiecesPool;
        [SerializeField] PlatformPiecesPool m_Platform10PiecesPool;

        [Header("Debug")] 
        [SerializeField] CustomLogger m_Logger;

        int _nextPlatformPositionX;
        const int PlatformSize = 2;
        Queue<(int nextPositionX, Platform platform)> _spawnedPlatforms;
        int _firstPlatformInQueuePositionX;

        void Start() {
            _spawnedPlatforms = new Queue<(int, Platform)>();
            CalculateNextPlatformPosition((int)m_InitialPlatform.transform.position.x, 
                                            (int)m_InitialPlatform.NumberOfPieces);
            _firstPlatformInQueuePositionX = _nextPlatformPositionX;
            SpawnPlatform();
        }

        void Update() {
            if (_nextPlatformPositionX - m_MainCamera.position.x <= m_DistanceToSpawn) {
                m_Logger.Log($"Spawn new platform[platformPos: {_nextPlatformPositionX} | camPos: {m_MainCamera.position.x}]",this);
                SpawnPlatform();
            }

            if (m_MainCamera.position.x - _firstPlatformInQueuePositionX >= m_DistanceToSpawn * 2) {
                m_Logger.Log($"Free older platform[camPos: {m_MainCamera.position.x} | platformPos: {_firstPlatformInQueuePositionX}]", this);
                FreePlatform();
            }
        }

        void SpawnPlatform() {
            var platformGap = Random.Range(m_MinGap, m_MaxGap + 1);
            var spawnPositionX = _nextPlatformPositionX + platformGap;
            var newPlatform = GetRandomPlatform();
            newPlatform.transform.position = new Vector3(spawnPositionX,
                newPlatform.transform.position.y,
                newPlatform.transform.position.z);
            
            CalculateNextPlatformPosition((int)newPlatform.transform.position.x,
                                            (int)newPlatform.NumberOfPieces);
            _spawnedPlatforms.Enqueue((_nextPlatformPositionX, newPlatform));
        }

        void FreePlatform() {
            var platform = _spawnedPlatforms.Dequeue();
            ReleasePlatform(platform.platform);
            _firstPlatformInQueuePositionX = platform.nextPositionX;
        }

        Platform GetRandomPlatform() {
            var platformType = Random.Range(0, 6);
            return platformType switch {
                1 => m_Platform3PiecesPool.Allocate(true),
                2 => m_Platform4PiecesPool.Allocate(true),
                3 => m_Platform6PiecesPool.Allocate(true),
                4 => m_Platform8PiecesPool.Allocate(true),
                5 => m_Platform10PiecesPool.Allocate(true),
                _ => m_Platform2PiecesPool.Allocate(true)
            };
        }

        void ReleasePlatform(Platform platform) {
            switch ((int)platform.NumberOfPieces) {
                case 3:
                    m_Platform3PiecesPool.Release(platform);
                    break;
                case 4:
                    m_Platform4PiecesPool.Release(platform);
                    break;
                case 6:
                    m_Platform6PiecesPool.Release(platform);
                    break;
                case 8:
                    m_Platform8PiecesPool.Release(platform);
                    break;
                case 10:
                    m_Platform10PiecesPool.Release(platform);
                    break;
                default:
                    m_Platform2PiecesPool.Release(platform);
                    break;
            }
        }

        void CalculateNextPlatformPosition(int currentPlatformPositionX, int numberOfPieces) {
            _nextPlatformPositionX = currentPlatformPositionX + numberOfPieces * PlatformSize;
        }
    }
}
