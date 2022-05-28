using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace com.pepipe.Pool 
{
    public class Pool<T> : IEnumerable where T : IResettable
    {
        public List<T> Members => _members;
        public HashSet<T> Unavailable => _unavailable;

        readonly List<T> _members = new();
        readonly HashSet<T> _unavailable = new ();
        readonly bool _fixedPoolSize;
        readonly IFactory<T> _factory;
        readonly int _maxPoolSize;

        public Pool(IFactory<T> factory, 
            int poolSize = 10, 
            bool isPoolSizeFixed = false, 
            int maxPoolSize = 9999,
            bool startActive = false)
        {
            _factory = factory;
            _fixedPoolSize = isPoolSizeFixed;
            _maxPoolSize = maxPoolSize;
            for (var i = 0; i < poolSize; ++i)
                Create(startActive);
        }

        public T Allocate(bool startActive)
        {
            foreach (var member in _members.Where(member => !_unavailable.Contains(member)))
            {
                _unavailable.Add(member);
                if(startActive)
                    member.Activate();
                return member;
            }

            if (_fixedPoolSize || _members.Count >= _maxPoolSize) return default;
            
            var newMember = Create(startActive);
            _unavailable.Add(newMember);
            newMember.Activate();
            return newMember;
        }

        public void Release(T member)
        {
            member.Reset();
            _unavailable.Remove(member);
        }

        public void ReleaseAll()
        {
            foreach (var member in _members)
                Release(member);
        }
        
        IEnumerator IEnumerable.GetEnumerator()
        {
            return _members.GetEnumerator();
        }
        
        T Create(bool startActive) {
            var member = _factory.Create();
            if(startActive)
                member.Activate();
            else
                member.Reset();
            _members.Add(member);
            return member;
        }
    }
}