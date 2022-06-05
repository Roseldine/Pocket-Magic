
namespace StardropTools.Pool
{
    [System.Serializable]
    public class PoolData
    {
        public string name;
        public int id;

        public PoolData(string name, int id)
        {
            this.name = name;
            this.id = id;
        }

        public PoolData(int id, string name)
        {
            this.name = name;
            this.id = id;
        }
    }
}