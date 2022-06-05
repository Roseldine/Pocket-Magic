
using UnityEngine;

namespace StardropTools.Pool
{
    public class SingletonPoolManager<T> : CoreManager where T : Component
    {
        #region Manager Singleton
        /// <summary>
        /// The instance.
        /// </summary>
        private static T instance;


        /// <summary>
        /// Gets the instance.
        /// </summary>
        /// <value>The instance.</value>
        public static T Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = FindObjectOfType<T>();
                    if (instance == null)
                    {
                        GameObject obj = new GameObject();
                        obj.name = typeof(T).Name;
                        instance = obj.AddComponent<T>();
                    }
                }
                return instance;
            }
        }


        void SingletonInitialization()
        {
            if (instance == null)
            {
                instance = this as T;
                DontDestroyOnLoad(gameObject);
            }

            else
                Destroy(gameObject);
        }


        public override void Initialize()
        {
            base.Initialize();

            SingletonInitialization();

            if (populationInitialization == EPopulationInitialization.initialize)
                Populate();
        }
        #endregion // manager singleton

        public enum EPopulationInitialization { initialize, lateInitialize }

        [Header("Clusters")]
        [SerializeField] protected EPopulationInitialization populationInitialization;
#if UNITY_EDITOR
        [TextArea] [SerializeField] protected string description;
#endif
        [SerializeField] protected PoolCluster[] clusters;
        [SerializeField] protected bool getPools;

        public override void LateInitialize()
        {
            base.LateInitialize();

            if (populationInitialization == EPopulationInitialization.lateInitialize)
                Populate();
        }

        public void Populate()
        {
            GetClusters();
            if (clusters.Exists())
                for (int i = 0; i < clusters.Length; i++)
                    clusters[i].Initialize(i);
        }


        #region Spawn
        public PooledObject SpawnFromPoolCluster(int clusterIndex, int poolIndex, Vector3 position, Quaternion rotation, Transform parent)
            => clusters[clusterIndex].SpawnFromPool(poolIndex, position, rotation, parent);

        public T SpawnFromPoolCluster<T>(int clusterIndex, int poolIndex, Vector3 position, Quaternion rotation, Transform parent)
            => clusters[clusterIndex].SpawnFromPool<T>(poolIndex, position, rotation, parent);


        public PooledObject SpawnRandomFromPoolCluster(int clusterIndex, Vector3 position, Quaternion rotation, Transform parent)
            => clusters[clusterIndex].SpawnRandomFromPool(position, rotation, parent);

        public T SpawnRandomFromPoolCluster<T>(int clusterIndex, Vector3 position, Quaternion rotation, Transform parent)
            => clusters[clusterIndex].SpawnRandomFromPool<T>(position, rotation, parent);

        #endregion // spawn

        #region Despawn

        public void DespawnFromPool(int clusterIndex, int poolIndex, PooledObject pooledObject, bool resetParent)
            => clusters[clusterIndex].DespawnFromPool(poolIndex, pooledObject, resetParent);

        public void DespawnFromPool(int clusterIndex, int poolIndex, GameObject pooledObject, bool resetParent)
            => clusters[clusterIndex].DespawnFromPool(poolIndex, pooledObject, resetParent);

        public void DespawnClusterPool(int clusterIndex, int poolIndex) => clusters[clusterIndex].DespawnAllFromPool(poolIndex);

        public void DespawnEntireCluster(int clusterIndex) => clusters[clusterIndex].DespawnAllPools();

        public void Despawn(PooledObject pooled, bool resetParent = true)
        {
            var cluster = clusters[pooled.ClusterID];

            if (cluster == null)
            {
                WrongDespawnMessage(pooled);
                return;
            }

            if (cluster.ClusterName == pooled.ClusterName)
                cluster.Despawn(pooled, resetParent);
            else
                WrongDespawnMessage(pooled, cluster);
        }

        public void Despawn(GameObject pooled, bool resetParent = true)
            => Despawn(pooled.GetComponent<PooledObject>(), resetParent);

        //public void Despawn(PooledObject pooled, float delay, bool resetParent = true)
        //    => Invoke(nameof(Despawn(pooled, resetParent)), delay);

        public void DespawnEverything()
        {
            for (int i = 0; i < clusters.Length; i++)
                clusters[i].DespawnAllPools();
        }

        void WrongDespawnMessage(PooledObject pooled)
            => Debug.LogFormat("<color=yellow>Attempted to despawn object {0} of pool id: {1}, not spawned from cluster id: {2}</color>", pooled.name, pooled.PoolID, pooled.ClusterID);


        void WrongDespawnMessage(PooledObject pooled, PoolCluster cluster)
            => Debug.LogFormat("<color=yellow>Attempted to despawn object {0} of pool id: {1}, not spawned from cluster id: {2}</color>", pooled.name, pooled.PoolID, cluster.ClusterID);

        #endregion // despawn

        void GetClusters()
        {
            clusters = GetItems<PoolCluster>(Transform);
        }

        protected override void OnValidate()
        {
            base.OnValidate();

            if (getPools)
                GetClusters();
            getPools = false;

            if (clusters.Exists())
                for (int i = 0; i < clusters.Length; i++)
                    clusters[i].ClusterID = i;
        }
    }
}