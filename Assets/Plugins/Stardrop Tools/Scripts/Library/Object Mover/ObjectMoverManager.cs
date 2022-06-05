using System.Collections.Generic;
using UnityEngine;

namespace StardropTools
{
    public class ObjectMoverManager : Singleton<ObjectMoverManager>
    {
        [SerializeField] List<ObjectMover> listMovers;

        public void AddMover(ObjectMover mover)
        {
            if (listMovers.Contains(mover) == false)
                listMovers.Add(mover);
        }

        public void RemoveMover(ObjectMover mover)
        {
            if (listMovers.Contains(mover) == false)
                listMovers.Remove(mover);
        }

        public void Move(Vector3 direction)
        {
            if (listMovers.Exists())
                for (int i = 0; i < listMovers.Count; i++)
                {
                    listMovers[i].Move(direction);
                }
        }
    }
}