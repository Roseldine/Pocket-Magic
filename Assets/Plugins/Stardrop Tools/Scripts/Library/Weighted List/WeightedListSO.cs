using System.Collections;
using UnityEngine;

namespace StardropTools
{
    [CreateAssetMenu(menuName = "Stardrop / Weighted List")]
    public class WeightedListSO : ScriptableObject
    {
        [SerializeField] WeightedList<int> list;

        public int GetRandom() => list.GetRandom();

        public void Add(int item, float weight)
            => list.Add(new WeightedItem<int>(item, weight));

        public void Add(WeightedItem<int> item)
            => list.Add(item);

        public void Remove(WeightedItem<int> item)
            => list.Remove(item);
    }
}