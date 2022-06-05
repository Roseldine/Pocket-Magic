
using UnityEngine;

namespace StardropTools.Pool
{
    public interface IPool
    {
        public PooledObject Spawn(Vector3 position, Quaternion rotation, Transform parent);
        public T Spawn<T>(Vector3 position, Quaternion rotation, Transform parent);
        public void Despawn(PooledObject despawnable, bool resetParent = true);
        public void Despawn(GameObject pooledObject, bool resetParent = true);
        public void DespawnAll();
    }
}

