using System.Collections;
using UnityEngine;

namespace StardropTools.Combat
{
    [CreateAssetMenu(menuName = "Stardrop / Combat / Ranged Weapon Data")]
    public class RangedWeaponDataSO : ScriptableObject
    {
        [SerializeField] RangedWeaponData weaponData;
    }
}