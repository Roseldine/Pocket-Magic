

namespace StardropTools.Pool
{
    [System.Serializable]
    public abstract class Pool<T> where T : class
    {
        [UnityEngine.SerializeField] protected PoolData poolData;
        [UnityEngine.SerializeField] protected PoolData clusterData;
        [UnityEngine.Space]
        [UnityEngine.SerializeField] protected T template;
        [UnityEngine.SerializeField] protected int capacity;
        /// <summary>
        /// Can pool spawn more than amount?
        /// </summary>
        [UnityEngine.SerializeField] protected bool overflow;
        [UnityEngine.SerializeField] protected int overflowCount;
        [UnityEngine.Space]
        /// <summary>
        /// if != 0, despawn after lifetime is reached
        /// </summary>
        [UnityEngine.Min(0)][UnityEngine.SerializeField] protected float lifeTime;
        [UnityEngine.SerializeField] protected int active;

        protected System.Collections.Generic.Queue<T> queue;
        protected System.Collections.Generic.List<T> cache;
        bool isInitialized;

        #region Properties
        public string ClusterName { get => clusterData.name; set => clusterData.name = value; }
        public int ClusterID { get => clusterData.id; set => clusterData.id = value; }

        public string PoolName { get => poolData.name; set => poolData.name = value; }
        public int PoolID { get => poolData.id; set => poolData.id = value; }
        #endregion // properties


        public Pool(PoolData clusterData, PoolData poolData, T prefab, int capacity, bool overflow, bool populate = true)
        {
            this.clusterData = clusterData;
            this.poolData = poolData;

            this.template = prefab;
            this.capacity = capacity;
            this.overflow = overflow;

            if (populate)
                Populate();
        }

        public void Populate(PoolData clusterData, int poolIndex)
        {
            this.clusterData = clusterData;
            PoolID = poolIndex;

            Populate();
        }

        public void Populate()
        {
            if (isInitialized)
                return;

            queue = new System.Collections.Generic.Queue<T>();
            cache = new System.Collections.Generic.List<T>();

            for (int i = 0; i < capacity; i++)
            {
                //T pooled = System.Activator.CreateInstance<T>();
                T pooled = CreateClass();
                queue.Enqueue(pooled);
            }

            isInitialized = true;
        }



        public T Spawn()
        {
            if (active >= capacity && overflow)
            {
                //T pooled = System.Activator.CreateInstance<T>();
                T pooled = CreateClass();
                ActivateObject(pooled);

                if (cache.Contains(pooled) == false)
                    cache.Add(pooled);

                overflowCount++;
                return pooled;
            }

            else
            {
                // dequeue = get from queue list
                T pooled = queue.Dequeue();
                ActivateObject(pooled);

                if (cache.Contains(pooled) == false)
                    cache.Add(pooled);

                active++;
                return pooled;
            }
        }


        public void Despawn(T pooled)
        {
            // find pooled equivalent
            for (int i = 0; i < cache.Count; i++)
                if (cache[i].Equals(pooled))
                {
                    DeactivateObject(pooled);
                    queue.Enqueue(pooled);
                    break;
                }

             UnityEngine.Debug.LogWarning("Object didn't come from this pool!");
        }

        public void DespawnAll()
        {
            if (cache.Exists())
                for (int i = 0; i < cache.Count; i++)
                    Despawn(cache[i]);

            if (overflowCount > 0)
                for (int i = 0; i < overflowCount; i++)
                    queue.Dequeue();
        }

        protected abstract T CreateClass();
        protected abstract void ActivateObject(T pooled);
        protected abstract void DeactivateObject(T pooled);
    }
}
