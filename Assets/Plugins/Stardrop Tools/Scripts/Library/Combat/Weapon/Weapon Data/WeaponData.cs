using System.Collections;
using UnityEngine;

namespace StardropTools.Combat
{
    [System.Serializable]
    public class WeaponData
    {
        [Header("Name")]
        [SerializeField] string weaponName;
        [TextArea][SerializeField] string weaponDescription;

        public string WeaponName { get => weaponName; }
        public string WeaponDescription { get => weaponDescription; }
    }
}