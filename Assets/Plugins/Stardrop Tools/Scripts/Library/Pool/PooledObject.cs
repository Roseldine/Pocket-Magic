
using System.Threading.Tasks;


namespace StardropTools.Pool
{
    public class PooledObject : CoreObject, IPoolable
    {
        [UnityEngine.Header("Pooled Object")]
        [UnityEngine.SerializeField] int pooledID;
        [UnityEngine.Space]
        [UnityEngine.SerializeField] PoolData clusterData;
        [UnityEngine.SerializeField] PoolData poolData;
        
        GameObjectPool pool;

        public int PooledFinderID { get; private set; }

        public string ClusterName { get => clusterData.name; set => clusterData.name = value; }
        public int ClusterID { get => clusterData.id; set => clusterData.id = value; }

        public string PoolName { get => poolData.name; set => poolData.name = value; }
        public int PoolID { get => poolData.id; set => poolData.id = value; }

        public int PooledID { get => pooledID; set => pooledID = value; }
        public GameObjectPool Pool { get => pool; }


        protected override void Awake()
        {
            base.Awake();
            PooledFinderID = CoreComponentFinder.AddPooled(this);
        }

        public void Initialize(GameObjectPool pool, PoolData clusterData, PoolData poolData, int itemID, bool setActive = false)
        {
            this.pool = pool;
            this.clusterData = clusterData;
            this.poolData = poolData;
            this.pooledID = itemID;

            SetActive(setActive);
            Initialize();
        }

        public override void LifeTime(GameObjectPool pool, float time)
            => Invoke(nameof(Despawn), time);

        System.Collections.IEnumerator LifetimeCR(GameObjectPool pool, float time)
        {
            yield return pool.GetWait(time);
            pool.Despawn(this);
        }

        public async void LifeTimeAsync(GameObjectPool pool, float time)
            => await LifetimeSync(pool, time);

        private async Task LifetimeSync(GameObjectPool pool, float time)
        {
            int milisenconds = MathUtility.ConvertToMiliseconds(time);
            await Task.Delay(milisenconds);
            pool.Despawn(this);
        }

        public override void Despawn(bool resetParent = true)
        {
            pool.Despawn(this, resetParent);
        }
    }
}
