using System.Collections.Generic;
using com.pepipe.Pool;
using pepipe.DeathRun.Player;
using pepipe.Utils.Logging;
using UnityEngine;

namespace pepipe.DeathRun
{
    public class RoadSpawner : MonoBehaviour {
        [SerializeField] RoadPiece InitialRoad; 
        [SerializeField] GameObject m_RoadPrefab;
        [SerializeField] Player3DController m_PlayerController;
        [SerializeField] float m_RoadPieceZSpawn = 500f;
        
        [Header("Debug")] 
        [SerializeField] CustomLogger m_Logger;

        Pool<RoadPiece> _roadPiecesPool;
        Queue<RoadPiece> _roadPieces;
        int _piecesSpawned;

        void Start() {
            _roadPiecesPool = new Pool<RoadPiece>(new PrefabFactory<RoadPiece>(m_RoadPrefab, gameObject));
            _roadPieces = new Queue<RoadPiece>();
            _roadPieces.Enqueue(InitialRoad);
            m_PlayerController.RoadSpawn += OnRoadSpawn;
            m_PlayerController.RoadDespawn += OnRoadDespawn;
        }

        void OnDisable() {
            m_PlayerController.RoadSpawn -= OnRoadSpawn;
            m_PlayerController.RoadDespawn -= OnRoadDespawn;
        }

        void OnRoadSpawn() {
            ++_piecesSpawned;
            var roadPiece = _roadPiecesPool.Allocate(true);
            roadPiece.transform.position = new Vector3(
                transform.position.x,
                transform.position.y,
                m_RoadPieceZSpawn * _piecesSpawned + InitialRoad.transform.position.z
            );
            var zpos = m_RoadPieceZSpawn * _piecesSpawned + InitialRoad.transform.position.z;
            m_Logger.Log("ZPOS: " + zpos, this);
            _roadPieces.Enqueue(roadPiece);
        }

        void OnRoadDespawn() {
            var roadPiece = _roadPieces.Dequeue();
            if (roadPiece == InitialRoad) return;
            _roadPiecesPool.Release(roadPiece);
        }
    }
}
