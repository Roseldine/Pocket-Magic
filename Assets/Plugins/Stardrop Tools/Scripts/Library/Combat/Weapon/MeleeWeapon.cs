using System.Collections;
using UnityEngine;
using StardropTools.Pool;
using StardropTools.Audio;

namespace StardropTools.Combat
{
    public class MeleeWeapon : Weapon
    {
        [SerializeField] MeleeWeaponDataSO weaponData;

        [Header("Melee Impact Hit Boxes")]
        [SerializeField] Transform weaponImpactReferencePoint;
        [SerializeField] OverlapBoxScanner[] hitBoxes;

        [Header("Weapon Pools")]
        [SerializeField] protected PoolCluster pool_Muzzles;
        [SerializeField] protected PoolCluster pool_Impacts;

        [Header("Audio")]
        [SerializeField] AudioDatabaseSO swingAudio;
        [SerializeField] AudioDatabaseSO impactAudio;

        public override void Initialize()
        {
            base.Initialize();

            for (int i = 0; i < hitBoxes.Length; i++)
            {
                hitBoxes[i].Initialize();
                hitBoxes[i].OnEnter.AddListener(WeaponHit);
            }
        }

        protected override void WeaponHit(Collider collider)
        {
            WeaponHitData(weaponImpactReferencePoint, collider);
            OnHitDry?.Invoke();
        }

        protected override ImpactData WeaponHitData(Transform impactor, Collider collider)
        {
            ImpactData data = new ImpactData(this, impactor, collider);

            OnHit?.Invoke(data);
            return data;
        }

        

        public void UpdateMeleeWeapon()
        {
            for (int i = 0; i < hitBoxes.Length; i++)
                hitBoxes[i].Scan();
        }
    }
}