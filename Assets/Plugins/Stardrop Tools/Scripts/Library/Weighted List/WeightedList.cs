
namespace StardropTools
{
    [System.Serializable]
    public class WeightedList<T>
    {
        [UnityEngine.TextArea] [UnityEngine.SerializeField] string description;
        public System.Collections.Generic.List<WeightedItem<T>> itemList = new System.Collections.Generic.List<WeightedItem<T>>();

        public int Count { get => itemList.Count; }

        public void Add(T item, float weight)
            => itemList.Add(new WeightedItem<T>(item, weight));

        public void Add(WeightedItem<T> item)
        {
            if (itemList.Contains(item) == false)
                itemList.Add(item);
        }

        public void Remove(WeightedItem<T> item)
        {
            if (itemList.Contains(item) == false)
                itemList.Remove(item);
        }

        public T GetRandom()
        {
            if (itemList.Count == 0)
            {
                UnityEngine.Debug.Log("List is empty!");
                return default(T);
            }

            float totalWeight = 0;

            foreach (WeightedItem<T> item in itemList)
                totalWeight += item.weight;            

            float value = UnityEngine.Random.value * totalWeight;

            float sumWeight = 0;

            foreach (WeightedItem<T> item in itemList)
            {
                sumWeight += item.weight;

                if (sumWeight >= value)
                    return item.item;
            }

            return default(T);
        }
    }
}