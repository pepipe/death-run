using System;
using UnityEngine;

namespace com.pepipe.Pool.UI
{
    [Serializable]
    public class PoolGameObject<T> : MonoBehaviour, IPoolGameObject<T> where T : MonoBehaviour, IResettable
    {
        [Tooltip("Object to be instantiated into the pool")]
        [SerializeField] GameObject m_PoolObjectPrefab;

        [Header("Pool Settings")]
        [SerializeField] int m_PoolSize = 1;
        [SerializeField] bool m_PoolSizeFixed = false;
        [SerializeField] int m_MaxPoolSize = 9999;
        [SerializeField] bool m_StartActive = true;
        
        Pool<T> _pool;

        void Awake()
        {
            _pool = CreatePool();
        }

        public Pool<T> CreatePool()
        {
            return _pool ??= new Pool<T>(
                new PrefabFactory<T>(m_PoolObjectPrefab, gameObject),
                m_PoolSize, m_PoolSizeFixed, m_MaxPoolSize, m_StartActive);
        }

        public T Allocate(bool startActive)
        {
            return _pool.Allocate(startActive);
        }

        public void Release(T poolObject)
        {
            _pool.Release(poolObject);
        }
    }
}
