using System.Collections.Generic;
using UnityEngine;
using StardropTools.Pool;

namespace StardropTools
{
    public static class CoreComponentFinder
    {
        public static List<CoreComponent> coreComponents = new List<CoreComponent>();
        public static List<PooledObject> pooledComponents = new List<PooledObject>();

        public static int AddCoreComponent(CoreComponent component)
        {
            if (coreComponents.Contains(component) == false)
                coreComponents.Add(component);

            return coreComponents.Count - 1;
        }

        /// <summary>
        /// Using this might make problems with getting component via indexID
        /// </summary>
        public static void RemoveCoreComponent(CoreComponent component)
            => coreComponents.Remove(component);

        public static CoreComponent GetCoreComponent(GameObject referenceObject, int index)
        {
            if (referenceObject.GetInstanceID() == coreComponents[index].GetInstanceID())
                return coreComponents[index];
            else
                return null;
        }

        public static CoreComponent GetCoreComponentByInstanceID(int instanceID)
        {
            CoreComponent component = null;

            for (int i = 0; i < coreComponents.Count; i++)
            {
                if (coreComponents[i].GetInstanceID() == instanceID)
                {
                    component = coreComponents[i];
                    break;
                }
            }

            if (component == null)
                Debug.Log("No CoreComponent found with instance id: " + instanceID);

            return component;
        }


        public static int AddPooled(PooledObject pooled)
        {
            if (pooledComponents.Contains(pooled) == false)
                pooledComponents.Add(pooled);

            return pooledComponents.Count - 1;
        }

        /// <summary>
        /// Using this might make problems with getting component via indexID
        /// </summary>
        public static void RemovePooled(PooledObject pooled)
            => pooledComponents.Remove(pooled);

        public static PooledObject GetPooledComponent(GameObject referenceObject, int index)
        {
            if (referenceObject.GetInstanceID() == pooledComponents[index].GetInstanceID())
                return pooledComponents[index];
            else
                return null;
        }

        public static PooledObject GetPooledByInstanceID(int instanceID)
        {
            PooledObject pooled = null;

            for (int i = 0; i < coreComponents.Count; i++)
            {
                if (pooledComponents[i].GetInstanceID() == instanceID)
                {
                    pooled = pooledComponents[i];
                    break;
                }
            }

            if (pooled == null)
                Debug.Log("No CoreComponent found with instance id: " + instanceID);

            return pooled;
        }
    }
}