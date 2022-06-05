using System.Collections;
using UnityEngine;
using StardropTools.Audio;

namespace StardropTools.Combat
{
    [System.Serializable]
    public class RangedWeaponData : WeaponData
    {
        [Header("Weapon Data")]
        [SerializeField] protected int damage = 2;
        [SerializeField] protected int maxDamage = 5;
        [Tooltip("0 for infinite magazine")]
        [SerializeField] protected int magazineSize = 30;
        [Tooltip("0 for infinite magazine reserves")]
        [SerializeField] protected int magazineReserve = 2;
        [Space]
        [SerializeField] protected float roundsPerSecond = 6.5f;
        [SerializeField] protected float reloadTime = 1.5f;
        [SerializeField] protected float equipTime = 1f;
        [Space]
        [SerializeField] protected bool isHitScan;

        [Header("Prefabs for pool generation")]
        [SerializeField] protected GameObject projectilePrefab;
        [SerializeField] protected GameObject muzzlePrefab;
        [SerializeField] protected GameObject impactPrefab;

        [Header("Audio")]
        [SerializeField] protected AudioDatabaseSO shootAudio;
        [SerializeField] protected AudioDatabaseSO impactAudio;

        [Header("Animation")]
        [SerializeField] protected Animator stanceAnimator;

        public int Damage { get => damage; }
        public int MaxDamage { get => maxDamage; }
        public int MagazineSize { get => magazineSize; }
        public int MagazineReserve { get => magazineReserve; }
        public float RoundsPerSecond { get => roundsPerSecond; }
        public float ReloadTime { get => reloadTime; }
        public float EquipTime { get => equipTime; }
        public bool IsHitScan { get => isHitScan; }

        public GameObject ProjectilePrefab { get => projectilePrefab; }
        public GameObject MuzzlePrefab { get => muzzlePrefab; }
        public GameObject ImpactPrefab { get => impactPrefab; }

        public Animator StanceAnimator { get => stanceAnimator; }

    }
}