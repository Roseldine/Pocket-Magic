using System.Collections;
using UnityEngine;
using StardropTools.Pool;

namespace StardropTools.Combat
{
    public class Attack : CoreObjectEventStateMachined, IPoolable
    {
        [Header("Pooled Properties")]
        [SerializeField] protected PooledObject pooled;
        [SerializeField] protected bool findPooledOnAwake = true;

        [Header("Attack Properties")]
        [SerializeField] protected Weapon sourceWeapon;

        protected override void Awake()
        {
            base.Awake();

            if (findPooledOnAwake)
                pooled = CoreComponentFinder.GetPooledByInstanceID(GetInstanceID());
        }
    }
}