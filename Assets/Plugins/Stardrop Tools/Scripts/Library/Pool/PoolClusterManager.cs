
using UnityEngine;

namespace StardropTools.Pool
{
    public class PoolClusterManager : MonoBehaviour
    {
        [SerializeField] PoolData managerData;
        [SerializeField] WeightedList<int> poolWeights;
        [SerializeField] PoolCluster[] clusters;

        bool isInitialized;

        public string ClusterName { get => managerData.name; set => managerData.name = value; }
        public int ClusterID { get => managerData.id; set => managerData.id = value; }
        public int ClusterCount { get => clusters.Length; }

        public void Initialize(int clusterIndex)
        {
            if (isInitialized)
                return;

            managerData.id = clusterIndex;
            for (int i = 0; i < clusters.Length; i++)
                clusters[i].Initialize(i);

            isInitialized = true;
        }

        public PooledObject SpawnFromCluster(int poolIndex, Vector3 position, Quaternion rotation, Transform parent)
            => clusters[poolIndex].SpawnFromPool(poolIndex, position, rotation, parent);

        public T SpawnFromCluster<T>(int poolIndex, Vector3 position, Quaternion rotation, Transform parent)
            => clusters[poolIndex].SpawnFromPool<T>(poolIndex, position, rotation, parent);


        public PooledObject SpawnRandomFromCluster(Vector3 position, Quaternion rotation, Transform parent)
        {
            if (poolWeights.Count == 0)
                return clusters[Random.Range(0, clusters.Length)].SpawnRandomFromPool(position, rotation, parent);
            else
                return clusters[poolWeights.GetRandom()].SpawnRandomFromPool(position, rotation, parent);
        }

        public T SpawnRandomFromCluster<T>(Vector3 position, Quaternion rotation, Transform parent)
        {
            if (poolWeights.Count == 0)
                return clusters[Random.Range(0, clusters.Length)].SpawnRandomFromPool<T>(position, rotation, parent);
            else
                return clusters[poolWeights.GetRandom()].SpawnRandomFromPool<T>(position, rotation, parent);
        }


        public void DespawnFromCluster(int clusterIndex, PooledObject pooled, bool resetParent = true)
            => clusters[clusterIndex].Despawn(pooled, resetParent);

        public void DespawnFromCluster(int clusterIndex, int poolIndex, GameObject pooledObject, bool resetParent = true)
            => clusters[clusterIndex].DespawnFromPool(poolIndex, pooledObject, resetParent);

        public void Despawn(PooledObject pooled, bool resetParent = true)
        {
            var cluster = clusters[pooled.PoolID];

            if (cluster == null)
            {
                WrongDespawnMessage(pooled);
                return;
            }

            if (cluster.ClusterName == pooled.ClusterName)
                cluster.Despawn(pooled, resetParent);
            else
                WrongDespawnMessage(pooled, this);
        }

        public void DespawnAllFromCluster(int clusterIndex) => clusters[clusterIndex].DespawnAllPools();

        public void DespawnAllClusters()
        {
            foreach (PoolCluster cluster in clusters)
                cluster.DespawnAllPools();
        }

        void WrongDespawnMessage(PooledObject pooled)
            => Debug.LogFormat("<color=yellow>Attempted to despawn object {0} of pool id: {1}, not spawned from cluster id: {2}</color>", pooled.name, pooled.PoolID, pooled.ClusterID);


        void WrongDespawnMessage(PooledObject pooled, PoolClusterManager manager)
            => Debug.LogFormat("<color=yellow>Attempted to despawn object {0} of pool id: {1}, not spawned from cluster id: {2}</color>", pooled.name, pooled.PoolID, manager.ClusterID);

        public void RefreshCusterNames()
        {
            if (clusters.Exists())
                for (int i = 0; i < clusters.Length; i++)
                {
                    clusters[i].ClusterID = ClusterID;
                    clusters[i].ClusterName = ClusterName;

                    clusters[i].RefreshPoolNames();
                }
        }

        private void OnValidate()
        {
            RefreshCusterNames();
        }
    }
}