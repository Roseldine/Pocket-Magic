using System.Collections.Generic;
using UnityEngine;

namespace StardropTools
{
    [CreateAssetMenu(menuName = "Stardrop / Team / Team")]
    public class TeamSO : ScriptableObject
    {
        [SerializeField] Team team;

        public Team Team { get => team; }

        public string TeamName { get => team.Name; }
        public Color TeamColor { get => team.Color; }
        public int TeamIndex { get => team.Index; }
    }
}