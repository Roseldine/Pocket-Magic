using System.Collections;
using UnityEngine;

namespace StardropTools
{
    [CreateAssetMenu(menuName = "Stardrop / Team / Teams")]
    public class TeamsSO : ScriptableObject
    {
        [SerializeField] System.Collections.Generic.List<Team> teams;

        public Team GetTeam(int teamIndex)
            => teams[teamIndex];

        public Team GetTeam(string teamName)
        {
            for (int i = 0; i < teams.Count; i++)
            {
                if (teams[i].Name.Equals(teamName))
                    return teams[i];
            }

            return Team.NullTeam;
        }

        public Team GetTeam(Color teamColor)
        {
            for (int i = 0; i < teams.Count; i++)
            {
                if (teams[i].Color.Equals(teamColor))
                    return teams[i];
            }

            return Team.NullTeam;
        }

        public void AddTeam(Team newTeam)
        {
            if (teams != null && teams.Count > 0)
                for (int i = 0; i < teams.Count; i++)
                {
                    if (teams[i].Index.Equals(newTeam.Index) == false && teams[i].Name.Equals(newTeam.Name) == false &&
                        teams[i].Color.Equals(newTeam.Color) == false)
                        teams.Add(newTeam);
                }

            else
            {
                teams = new System.Collections.Generic.List<Team>();
                teams.Add(newTeam);
            }
        }
    }
}