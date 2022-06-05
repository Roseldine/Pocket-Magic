using System.Collections;
using UnityEngine;
using StardropTools.Audio;

namespace StardropTools.Combat
{
    [System.Serializable]
    public class MeleeWeaponData : WeaponData
    {
        [SerializeField] int damage = 2;
        [SerializeField] int maxDamage = 5;

        [Header("Audio")]
        [SerializeField] AudioDatabaseSO swingAudio;
        [SerializeField] AudioDatabaseSO impactAudio;

        public int Damage { get => damage; }
        public int MaxDamage { get => maxDamage; }
    }
}