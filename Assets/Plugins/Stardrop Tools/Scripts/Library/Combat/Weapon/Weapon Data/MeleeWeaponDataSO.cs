using System.Collections;
using UnityEngine;

namespace StardropTools.Combat
{
    [CreateAssetMenu(menuName = "Stardrop / Combat / Melee Weapon Data")]
    public class MeleeWeaponDataSO : ScriptableObject
    {
        [SerializeField] MeleeWeaponData weaponData;
    }
}