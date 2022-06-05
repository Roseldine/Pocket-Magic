using System.Collections;
using UnityEngine;

namespace StardropTools.Combat
{
    public abstract class Weapon : CoreObjectEventStateMachined
    {
        [Header("Weapon")]
        [SerializeField] protected Transform targetParent;
        [SerializeField] protected AudioSource audioSource;

        public readonly CoreEvent<ImpactData> OnHit = new CoreEvent<ImpactData>();
        public readonly CoreEvent OnHitDry = new CoreEvent();

        protected abstract void WeaponHit(Collider collider);
        protected abstract ImpactData WeaponHitData(Transform impactor, Collider collider);
    }
}