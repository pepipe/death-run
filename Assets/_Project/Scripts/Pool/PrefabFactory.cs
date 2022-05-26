using UnityEngine;

namespace com.pepipe.Pool
{
    public class PrefabFactory<T> : IFactory<T> where T : MonoBehaviour
    {
        public int ObjectsInstantiated => _index;

        int _index;
        readonly GameObject _prefab;
        readonly string _name;
        readonly Transform _parent;
        
        public PrefabFactory(GameObject prefab, GameObject parent = null) : this(prefab, prefab.name, parent){}

        PrefabFactory(GameObject prefab, string name, GameObject parent)
        {
            _prefab = prefab;
            _name = name;
            _parent = parent == null ? prefab.transform.parent : parent.transform;
        }
        public T Create()
        {
            var tempGameObject = _parent != null ? 
                Object.Instantiate(_prefab, _parent.transform) : 
                Object.Instantiate(_prefab);
            
            tempGameObject.name = _name + _index;
            var objectOfType = tempGameObject.GetComponent<T>();
            _index++;

            return objectOfType;
        }
    }
}