using System.Collections;
using UnityEngine;

namespace StardropTools.Combat
{
    [System.Serializable]
    public class ImpactData
    {
        [SerializeField] Weapon sourceWeapon;
        [SerializeField] GameObject impactedObject;
        [SerializeField] CoreComponent impactedCoreComponent;
        [SerializeField] Vector3 impactDirection;
        [SerializeField] Vector3 impactPosition;

        public Weapon SourceWeapon { get => sourceWeapon; }
        public GameObject ImpactedObject { get => impactedObject; }
        public CoreComponent ImpactedCoreComponent { get => impactedCoreComponent; }
        public Vector3 ImpactDirection { get => impactDirection; }
        public Vector3 ImpactPosition { get => impactPosition; }

        public ImpactData(Weapon sourceWeapon, Transform impactor, Collider impactedCollider, bool findCoreComponent = false)
        {
            this.sourceWeapon = sourceWeapon;

            impactDirection = impactor.forward;

            impactedObject = impactedCollider.gameObject;

            impactPosition = impactedCollider.ClosestPoint(impactor.position);

            if (findCoreComponent)
                impactedCoreComponent = CoreComponentFinder.GetCoreComponentByInstanceID(impactedCollider.GetInstanceID());
        }
    }
}