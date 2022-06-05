using System.Collections;
using UnityEngine;
using StardropTools.Pool;
using StardropTools.FiniteStateMachine.EventFiniteStateMachine;

namespace StardropTools.Combat
{
    public class RangedWeapon : Weapon
    {
        [SerializeField] protected RangedWeaponDataSO weaponData;
        [SerializeField] protected ProjectileTrajectory trajectory;
        [SerializeField] protected Transform muzzle;

        [Header("Weapon Pools")]
        [Tooltip("Ex: 0-projectile, 1-projectileImpact, 3-muzzle")]
        [SerializeField] protected PoolCluster weaponPool;

        public override void Initialize()
        {
            CreateEventStates();
            base.Initialize();
        }

        public override void CreateEventStates()
        {
            EventState[] states =
            {
                new EventState(0, "Ready"),
                new EventState(1, "Shoot", 2),
                new EventState(2, "Cooldown", 0),
                new EventState(3, "Reload", 0),
            };

            eventStateMachine.EventStateMachine = new EventStateMachine(states);
        }

        protected override void WeaponHit(Collider collider)
        {
            //WeaponHitData(collider);
            OnHitDry?.Invoke();
        }

        protected override ImpactData WeaponHitData(Transform impactor, Collider collider)
        {
            ImpactData data = new ImpactData(this, impactor, collider);

            OnHit?.Invoke(data);
            return data;
        }


        //////////////////////
        // State Machine Logic
        //////////////////////

        #region Ready State

        #endregion // ready states
    }
}